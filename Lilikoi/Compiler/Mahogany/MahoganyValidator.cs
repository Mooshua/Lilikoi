﻿//       ========================
//       Lilikoi::MahoganyValidator.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 23.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using System.Reflection;

using Lilikoi.Attributes.Builders;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Compiler.Mahogany;

internal static class MahoganyValidator
{
	internal static bool ValidInjectable(LkInjectionBuilderAttribute attribute, Type test, Mount mount)
	{
		//	This is REAL C# done by REAL C# programmers!
		//	Look at what they have been doing behind our backs!!!
		return (bool)(attribute.GetType().GetMethod("IsInjectable")
			?.MakeGenericMethod(test)
			?.Invoke(attribute, new[] { mount }) ?? false);
	}

	internal static void ValidateInjection(LkInjectionBuilderAttribute attribute, FieldInfo fieldInfo, Mount mount)
	{
		if (!ValidInjectable(attribute, fieldInfo.FieldType, mount))
			throw new InvalidCastException($"Injectable '{attribute.GetType().FullName}'" +
			                               $" is unable to inject type '{fieldInfo.FieldType.FullName}'" +
			                               $" into property '{fieldInfo.Name}'" +
			                               $" of '{fieldInfo.DeclaringType.FullName}'");
	}

	internal static bool ValidParameter(LkParameterBuilderAttribute attribute, Type input, Type output, Mount mount)
	{
		return (bool)(attribute.GetType().GetMethod("IsInjectable")
			?.MakeGenericMethod(output, input)
			?.Invoke(attribute, new[] { mount }) ?? false);
	}

	internal static void ValidateParameter(LkParameterBuilderAttribute attribute, MethodInfo method, Type input, ParameterInfo parameterInfo, Mount mount)
	{
		if (!ValidParameter(attribute, input, parameterInfo.ParameterType, mount))
			throw new InvalidCastException($"Parameter injection '{attribute.GetType().FullName}'" +
			                               $" on parameter '{parameterInfo.Name}'" +
			                               $" on method '{method.Name}'" +
			                               $" rejected input-output pair '{input.FullName}'(in)" +
			                               $" '{parameterInfo.ParameterType.FullName}'(out)");
	}

	internal static bool ValidWrap(LkWrapBuilderAttribute attribute, Type input, Type output, Mount mount)
	{
		return (bool)(attribute.GetType().GetMethod("IsWrappable")
			?.MakeGenericMethod(input, output)
			?.Invoke(attribute, new[] { mount }) ?? false);
	}

	internal static void ValidateWrap(LkWrapBuilderAttribute attribute, MahoganyMethod method)
	{
		if (!ValidWrap(attribute, method.Input, method.Return, method.Mount))
			throw new InvalidCastException($"Wrap '{attribute.GetType().FullName}' " +
			                               $"rejected input-output pair on method '{method.Entry.Name}' " +
			                               $"with input of {method.Input.FullName} " +
			                               $"and inner container output of {method.Return.FullName} ");
	}
}
