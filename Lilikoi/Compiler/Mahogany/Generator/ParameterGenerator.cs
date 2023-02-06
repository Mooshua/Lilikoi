//       ========================
//       Lilikoi::ParameterGenerator.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using System.Linq.Expressions;
using System.Reflection;

using Lilikoi.Attributes;
using Lilikoi.Attributes.Builders;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Compiler.Mahogany.Generator;

internal static class ParameterGenerator
{
	internal static MethodInfo LkParameterBuilderAttribute_Build = typeof(LkParameterBuilderAttribute).GetMethod("Build");

	internal static MethodInfo LkParameterAttribute_Inject = typeof(LkParameterAttribute).GetMethod("Inject");
	internal static MethodInfo LkParameterAttribute_IsInjectable = typeof(LkParameterAttribute).GetMethod("IsInjectable");

	#region Builder

	internal static Expression Builder(LkParameterBuilderAttribute builderAttribute, Mount mount)
	{
		return
			Expression.Call(
				Expression.Constant(builderAttribute, typeof(LkParameterBuilderAttribute)),
				LkParameterBuilderAttribute_Build, Expression.Constant(mount));
	}

	#endregion

	#region Inject

	public static Expression Inject(MahoganyMethod method, Expression source, Type parameter, out ParameterExpression variable)
	{
		var invoke = LkParameterAttribute_Inject.MakeGenericMethod(parameter, method.Input);

		var inject = Expression.Call(
			source, invoke,
			method.Named(MahoganyConstants.MOUNT_VAR),
			method.Named(MahoganyConstants.INPUT_VAR)
			);

		var setter = method.AsVariable(inject, out variable);

		return setter;
	}

	public static Expression InjectParameter(MahoganyMethod method, Expression source, ParameterInfo info)
	{
		//	var param_x = source.Inject<Input, Parameter>()

		var setter = Inject(method, source, info.ParameterType, out var variable);

		method.MethodInjects[info] = variable;

		return setter;
	}

	public static Expression InjectWildcard(MahoganyMethod method, Expression source, Type wildcardType)
	{
		//	var param_x = source.Inject<Input, Parameter>()

		var setter = Inject(method, source, wildcardType, out var variable);

		method.Wildcards[wildcardType] = variable;

		return setter;
	}

	#endregion
}