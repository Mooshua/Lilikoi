//       ========================
//       Lilikoi.Core::TypeInjector.cs
//       Distributed under the MIT License.
// 
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
// 
// ->    Purpose:
// 
// 
//       ========================


/*
namespace Lilikoi.Attributes.Builder.Ocean;

internal static class TypeInjector
{
	public const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

	internal static bool IsInjectable(MkInjectionBuilderAttribute attribute, Type test)
	{
		//	This is REAL C# done by REAL C# programmers!
		//	Look at what they have been doing behind our backs!!!
		return (bool) (attribute.GetType().GetMethod("IsInjectable")
			?.MakeGenericMethod(test)
			?.Invoke(attribute, new object[0]) ?? false);

	}

	internal static bool IsWrappable(MkWrapBuilderAttribute attribute, Type input, Type output)
	{

		var inputOk = (bool) (attribute.GetType().GetMethod("IsAcceptableInput")
			?.MakeGenericMethod(input)
			?.Invoke(attribute, new object[0]) ?? false);

		var outputOk = (bool) (attribute.GetType().GetMethod("IsAcceptableOutput")
			?.MakeGenericMethod(output)
			?.Invoke(attribute, new object[0]) ?? false);

		return inputOk && outputOk;
	}

	public static List<ContainerInjectStep> InjectStepBuilder(Type type)
	{
		List<ContainerInjectStep> steps = new List<ContainerInjectStep>();

		foreach (PropertyInfo propertyInfo in type.GetProperties())
		{
			foreach (var attribute in propertyInfo.GetCustomAttributes()
				         .Where(obj => obj.GetType().IsSubclassOf(typeof(MkInjectionBuilderAttribute))))
			{
				try
				{
					var builders = (MkInjectionBuilderAttribute)attribute;

					if (!IsInjectable(builders, propertyInfo.PropertyType))
						throw new InvalidCastException($"Injectable '{builders.GetType().FullName}' is unable to inject type '{propertyInfo.PropertyType.FullName}' into property '{propertyInfo.Name}' of '{type.FullName}'");

					var step = new ContainerInjectStep(builders, propertyInfo);

					steps.Add(step);
				}
				catch (Exception e)
				{
					throw new AggregateException($"Unable to process '{attribute.GetType().FullName}'s injection of property '{propertyInfo.Name}' for type '{type.FullName}':", e);
				}
			}
		}

		return steps;
	}

	public static List<ContainerWrapStep> WrapStepBuilder(MethodInfo method)
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

}
*/