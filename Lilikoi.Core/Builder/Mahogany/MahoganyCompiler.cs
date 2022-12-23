//       ========================
//       Lilikoi.Core::MahoganyCompiler.cs
//       Distributed under the MIT License.
// 
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
// 
// ->    Purpose:
// 
// 
//       ========================
#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using Lilikoi.Core.Attributes.Builders;
using Lilikoi.Core.Generator;

#endregion

namespace Lilikoi.Core.Builder.Mahogany;

public class MahoganyCompiler
{
	public MahoganyMethod Method { get; set; }

	public MahoganyBlockStack Stack { get; set; }

	#region Utilities

	public const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

	internal static bool IsInjectable(MkInjectionBuilderAttribute attribute, Type test)
	{
		//	This is REAL C# done by REAL C# programmers!
		//	Look at what they have been doing behind our backs!!!
		return (bool)(attribute.GetType().GetMethod("IsInjectable")
			?.MakeGenericMethod(test)
			?.Invoke(attribute, new object[0]) ?? false);
	}

	internal static bool IsWrappable(MkWrapBuilderAttribute attribute, Type input, Type output)
	{
		var inputOk = (bool)(attribute.GetType().GetMethod("IsAcceptableInput")
			?.MakeGenericMethod(input)
			?.Invoke(attribute, new object[0]) ?? false);

		var outputOk = (bool)(attribute.GetType().GetMethod("IsAcceptableOutput")
			?.MakeGenericMethod(output)
			?.Invoke(attribute, new object[0]) ?? false);

		return inputOk && outputOk;
	}

	public static List<MahoganyInjectStep> InjectStepBuilder(Type host, MahoganyMethod method)
	{
		var steps = new List<MahoganyInjectStep>();

		foreach (var propertyInfo in host.GetProperties())
		foreach (var attribute in propertyInfo.GetCustomAttributes()
			         .Where(obj => obj.GetType().IsSubclassOf(typeof(MkInjectionBuilderAttribute))))
			try
			{
				var builders = (MkInjectionBuilderAttribute)attribute;

				if (!IsInjectable(builders, propertyInfo.PropertyType))
					throw new InvalidCastException($"Injectable '{builders.GetType().FullName}' is unable to inject type '{propertyInfo.PropertyType.FullName}' into property '{propertyInfo.Name}' of '{host.FullName}'");

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
	public static List<Expression> ParameterFillingForMethod(
		Dictionary<ParameterInfo, ParameterExpression> param,
		Dictionary<Type, ParameterExpression> wildcards,
		MethodInfo runtimeMethod)
	{
		var fill = new Dictionary<int, Expression>();

		foreach (var parameterInfo in runtimeMethod.GetParameters())
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
		var expressions = fill.Select(kv => (kv.Key, kv.Value))
			.ToList();

		expressions.Sort((a, b) => a.Key.CompareTo(b.Key));

		return expressions.Select(obj => obj.Value).ToList();
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

	#endregion

	#region Compiler

	public void InjectionsFor(Type host)
	{
		var steps = InjectStepBuilder(host, Method);

		foreach (var mahoganyInjectStep in steps)
		{
			(var enter, var exit) = mahoganyInjectStep.Generate();
			Stack.Push(enter, exit);
		}
	}

	public void ParameterSafety()
	{
		Method.Append(
			CommonGenerator.GuardAgainstNull(Method.Named(MahoganyConstants.HOST_VAR), new ArgumentNullException("__host", "Host cannot be null!"))
			);
	}

	public void Apex()
	{
		var filled = ParameterFillingForMethod(Method.MethodInjects, new Dictionary<Type, ParameterExpression>(), Method.Entry);

		Stack.Apex(
			Expression.Block(
				new[] { Method.Named(MahoganyConstants.OUTPUT_VAR) },
				Expression.Assign(Method.Named(MahoganyConstants.OUTPUT_VAR),
					Expression.Call(Method.Named(MahoganyConstants.HOST_VAR), Method.Entry, filled))
				)
			);
	}

	#endregion
}