//       ========================
//       Lilikoi.Core::ParameterGenerator.cs
//       Distributed under the MIT License.
//
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
//
// ->    Purpose:
//
//
//       ========================

using System.Linq.Expressions;
using System.Reflection;

using Lilikoi.Core.Attributes;
using Lilikoi.Core.Attributes.Builders;

namespace Lilikoi.Core.Builder.Mahogany.Generator;

internal static class ParameterGenerator
{
	internal static MethodInfo LkParameterBuilderAttribute_Build = typeof(LkParameterBuilderAttribute).GetMethod("Build");

	internal static MethodInfo LkParameterAttribute_Inject = typeof(LkParameterAttribute).GetMethod("Inject");
	internal static MethodInfo LkParameterAttribute_IsInjectable = typeof(LkParameterAttribute).GetMethod("IsInjectable");

	#region Builder

	internal static Expression Builder(LkParameterBuilderAttribute builderAttribute)
	{
		return
			Expression.Call(
				Expression.Constant(builderAttribute, typeof(LkInjectionBuilderAttribute)),
				LkParameterBuilderAttribute_Build);
	}

	#endregion

	#region Inject

	public static Expression Inject(MahoganyMethod method, Expression source, ParameterInfo info)
	{
		//	var param_x = source.Inject<Input, Parameter>()

		var invoke = LkParameterAttribute_Inject.MakeGenericMethod(info.ParameterType, method.Input);

		var inject = Expression.Call(
			source, invoke,
			method.Named(MahoganyConstants.MOUNT_VAR),
			method.Named(MahoganyConstants.INPUT_VAR)
			);

		var setter = method.AsVariable(inject, out var variable);

		method.MethodInjects[info] = variable;

		return setter;
	}

	#endregion

}
