//       ========================
//       Lilikoi::MahoganyCompiler.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using System.Linq.Expressions;
using System.Reflection;

using Lilikoi.Attributes.Builders;
using Lilikoi.Attributes.Static;
using Lilikoi.Compiler.Mahogany.Generator;
using Lilikoi.Compiler.Mahogany.Steps;

#endregion

namespace Lilikoi.Compiler.Mahogany;

public class MahoganyCompiler
{
	public MahoganyMethod Method { get; set; }

	public MahoganyBlockStack Stack { get; set; }

	#region Utilities

	public const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

	public static List<MahoganyParameterStep> ParameterStepBuilder(MahoganyMethod method)
	{
		var steps = new List<MahoganyParameterStep>();

		foreach (var parameterInfo in method.Entry.GetParameters())
		foreach (var attribute in parameterInfo.GetCustomAttributes()
			         .Where(obj => obj.GetType().IsSubclassOf(typeof(LkParameterBuilderAttribute))))
			try
			{
				var builders = (LkParameterBuilderAttribute)attribute;

				MahoganyValidator.ValidateParameter(builders, method.Entry, method.Input, parameterInfo, method.Mount);

				var step = new MahoganyParameterStep(method, parameterInfo, builders);

				steps.Add(step);
			}
			catch (Exception e)
			{
				throw new AggregateException($"Unable to process '{attribute.GetType().FullName}'s injection of parameter '{parameterInfo.Name}' for type '{method.Host.FullName}':", e);
			}

		return steps;
	}

	public static List<MahoganyWrapStep> WrapStepBuilder(MahoganyMethod method)
	{
		var steps = new List<MahoganyWrapStep>();

		foreach (var attribute in method.Entry.GetCustomAttributes()
			         .Where(obj => obj.GetType().IsSubclassOf(typeof(LkWrapBuilderAttribute))))
			try
			{
				var asBuilder = attribute as LkWrapBuilderAttribute;

				MahoganyValidator.ValidateWrap(asBuilder, method);

				var step = new MahoganyWrapStep(method, asBuilder);

				steps.Add(step);
			}
			catch (Exception e)
			{
				throw new AggregateException($"Unable to process '{attribute.GetType().FullName}'s wrap of '{method.Entry.Name}' for type '{method.Host.FullName}':", e);
			}

		return steps;
	}

	public static List<MahoganyInjectStep> InjectStepBuilder(Type host, MahoganyMethod method)
	{
		var steps = new List<MahoganyInjectStep>();

		foreach (var propertyInfo in host.GetFields(FLAGS))
		foreach (var attribute in propertyInfo.GetCustomAttributes()
			         .Where(obj => obj.GetType().IsSubclassOf(typeof(LkInjectionBuilderAttribute))))
			try
			{
				var builders = (LkInjectionBuilderAttribute)attribute;

				MahoganyValidator.ValidateInjection(builders, propertyInfo, method.Mount);

				var step = new MahoganyInjectStep(method, propertyInfo, builders);

				steps.Add(step);
			}
			catch (Exception e)
			{
				throw new AggregateException($"Unable to process '{attribute.GetType().FullName}'s injection of property '{propertyInfo.Name}' for type '{host.FullName}':", e);
			}

		return steps;
	}

	/// <summary>
	///     Fill the parameters of a method using several sources.
	/// </summary>
	/// <param name="param"></param>
	/// <param name="wildcards"></param>
	/// <param name="runtimeMethod"></param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public static Expression[] ParameterFillingForMethod(
		Dictionary<ParameterInfo, ParameterExpression> param,
		Dictionary<Type, ParameterExpression> wildcards,
		MethodInfo runtimeMethod)
	{
		var parameters = runtimeMethod.GetParameters();
		var fill = new Expression[parameters.Length];

		foreach (var parameterInfo in parameters)
		{
			if (param.ContainsKey(parameterInfo))
			{
				fill[parameterInfo.Position] = param[parameterInfo];
				continue;
			}

			if (wildcards.ContainsKey(parameterInfo.ParameterType))
			{
				fill[parameterInfo.Position] = wildcards[parameterInfo.ParameterType];
				continue;
			}

			throw new Exception($"No wildcard or parameter injection available for parameter at position {parameterInfo.Position} of method {runtimeMethod.Name} with type {parameterInfo.ParameterType.FullName}");
		}

		return fill;
	}
/*
	public static List<object> WrapStepBuilder(MethodInfo method)
	{
		List<ContainerWrapStep> steps = new List<ContainerWrapStep>();

		if (method.GetParameters().Length != 1)
			throw new InvalidOperationException($"Arguments to a container method must have one parameter. Method '{method.Name}' has {method.GetParameters().Length}");

		var param = method.GetParameters()[0];

		foreach (var attribute in method.GetCustomAttributes()
			         .Where(obj => obj.GetType().IsSubclassOf(typeof(MkWrapBuilderAttribute))))
		{
			try
			{
				var builders = (MkWrapBuilderAttribute)attribute;

				if (!IsWrappable(builders, param.ParameterType, method.ReturnType))
					throw new InvalidCastException($"Wrappable '{builders.GetType().FullName}' is unable to wrap return '{method.ReturnParameter.ParameterType.FullName}' or parameter '{param.ParameterType.FullName}'");

				var step = new ContainerWrapStep(builders);

				steps.Add(step);
			}
			catch (Exception e)
			{
				throw new AggregateException($"Unable to process '{attribute.GetType().FullName}'s wrap of method {method.Name}:", e);
			}
		}


		return steps;
	}
	*/

	public static List<LkMutatorAttribute> MutatorsForMethod(MethodInfo methodInfo)
	{
		return methodInfo.GetCustomAttributes().OfType<LkMutatorAttribute>().ToList();
	}

	#endregion

	#region Compiler

	public void WrapsFor()
	{
		var steps = WrapStepBuilder(Method);

		foreach (var mahoganyWrapStep in steps)
		{
			var (enter, exit) = mahoganyWrapStep.Generate();
			Stack.Push(enter, exit);
		}
	}

	public void ImplicitWrap(LkWrapBuilderAttribute builder)
	{
		var step = new MahoganyWrapStep(Method, builder);

		var (enter, exit) = step.Generate();
		Stack.Push(enter, exit);
	}

	public void ImplicitWildcard(LkParameterBuilderAttribute builder, Type type)
	{
		var step = new MahoganyParameterWildcardStep(Method, type, builder);

		var expression = step.Generate();
		Stack.Push(expression, Expression.Empty());
	}

	public void InjectionsFor(Type host)
	{
		var steps = InjectStepBuilder(host, Method);

		foreach (var mahoganyInjectStep in steps)
		{
			var (enter, exit) = mahoganyInjectStep.Generate();
			Stack.Push(enter, exit);
		}
	}

	public void ParametersFor()
	{
		var steps = ParameterStepBuilder(Method);

		foreach (var mahoganyParameterStep in steps)
		{
			var expression = mahoganyParameterStep.Generate();
			Stack.Push(expression, Expression.Empty());
		}
	}

	public void HostFor()
	{
		var step = new MahoganyCreateHostStep(Method);
		Stack.Push(step.Generate(), Expression.Empty());
	}

	public void ParameterSafety()
	{
		Method.Append(
			CommonGenerator.GuardAgainstNull(Method.Named(MahoganyConstants.HOST_VAR), new ArgumentNullException("__host", "Host cannot be null!"))
			);
	}

	public void Apex()
	{
		var filled = ParameterFillingForMethod(Method.MethodInjects, Method.Wildcards, Method.Entry);

		Stack.Apex(
			Expression.Block(
				Expression.Assign(Method.Named(MahoganyConstants.OUTPUT_VAR),
					Expression.Call(Method.Named(MahoganyConstants.HOST_VAR), Method.Entry, filled))
				)
			);
	}

	#endregion
}
