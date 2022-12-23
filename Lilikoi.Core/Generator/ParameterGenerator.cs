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

namespace Lilikoi.Core.Generator;

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


}
