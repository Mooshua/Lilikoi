//       ========================
//       Lilikoi.Core::MahoganyValidator.cs
//       Distributed under the MIT License.
//
// ->    Created: 23.12.2022
// ->    Bumped: 23.12.2022
//
// ->    Purpose:
//
//
//       ========================
using System;
using System.Reflection;

using Lilikoi.Core.Attributes.Builders;

namespace Lilikoi.Core.Builder.Mahogany;

internal static class MahoganyValidator
{
	internal static bool ValidInjectable(LkInjectionBuilderAttribute attribute, Type test)
	{
		//	This is REAL C# done by REAL C# programmers!
		//	Look at what they have been doing behind our backs!!!
		return (bool)(attribute.GetType().GetMethod("IsInjectable")
			?.MakeGenericMethod(test)
			?.Invoke(attribute, new object[0]) ?? false);
	}

	internal static void ValidateInjection(LkInjectionBuilderAttribute attribute, PropertyInfo propertyInfo)
	{
		if (!ValidInjectable(attribute, propertyInfo.PropertyType))
			throw new InvalidCastException($"Injectable '{attribute.GetType().FullName}'" +
			                               $" is unable to inject type '{propertyInfo.PropertyType.FullName}'" +
			                               $" into property '{propertyInfo.Name}'" +
			                               $" of '{propertyInfo.DeclaringType.FullName}'");
	}

	internal static bool ValidParameter(LkParameterBuilderAttribute attribute, Type input, Type output)
	{
		return (bool)(attribute.GetType().GetMethod("IsInjectable")
			?.MakeGenericMethod(output, input)
			?.Invoke(attribute, new object[0]) ?? false);
	}

	internal static void ValidateParameter(LkParameterBuilderAttribute attribute, MethodInfo method, Type input, ParameterInfo parameterInfo)
	{
		if (!ValidParameter(attribute, input, parameterInfo.ParameterType))
			throw new InvalidCastException($"Parameter injection '{attribute.GetType().FullName}'" +
			                               $" on parameter '{parameterInfo.Name}'" +
			                               $" on method '{method.Name}'" +
			                               $" rejected input-output pair '{input.FullName}'(in)" +
			                               $" '{parameterInfo.ParameterType.FullName}'(out)");
	}

	internal static bool ValidWrap(LkWrapBuilderAttribute attribute, Type input, Type output)
	{
		var inputOk = (bool)(attribute.GetType().GetMethod("IsAcceptableInput")
			?.MakeGenericMethod(input)
			?.Invoke(attribute, new object[0]) ?? false);

		var outputOk = (bool)(attribute.GetType().GetMethod("IsAcceptableOutput")
			?.MakeGenericMethod(output)
			?.Invoke(attribute, new object[0]) ?? false);

		return inputOk && outputOk;
	}


}
