//       ========================
//       Lilikoi.Core::WrapGenerator.cs
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

using Lilikoi.Attributes;
using Lilikoi.Attributes.Builders;
using Lilikoi.Context;

namespace Lilikoi.Compiler.Mahogany.Generator;

internal static class WrapGenerator
{

	internal static MethodInfo MkWrapBuilderAttribute_Build = typeof(LkWrapBuilderAttribute).GetMethod("Build");

	public static MethodInfo MkWrapAttribute_Before = typeof(LkWrapAttribute).GetMethod("Before");
	public static MethodInfo MkWrapAttribute_After = typeof(LkWrapAttribute).GetMethod("After");

	public const string WRAPRESULT_STOP = nameof(LkWrapAttribute.WrapResult<LkWrapAttribute>.stop);
	public const string WRAPRESULT_VALUE = nameof(LkWrapAttribute.WrapResult<LkWrapAttribute>.stopWithValue);

	/// <summary>
	/// Create an expression which creates an MkWrapAttribute.
	/// </summary>
	/// <param name="builderAttribute"></param>
	/// <returns></returns>
	internal static Expression Builder(LkWrapBuilderAttribute builderAttribute, Mount mount)
	{
		return Expression.Call(Expression.Constant(builderAttribute), MkWrapBuilderAttribute_Build, Expression.Constant(mount));
	}

	/// <summary>
	/// Create a redirectable "before" wrap which can modify the input or halt execution altogether.
	/// This wrap can jump to the "end" label provided in the builder.
	/// </summary>
	/// <param name="builder"></param>
	/// <param name="attribute">An expression which represents the MkWrapAttribute in use for this ececution.</param>
	/// <param name="inputSource"></param>
	/// <param name="input"></param>
	/// <param name="output"></param>
	/// <returns></returns>
	internal static Expression Before(MahoganyMethod method, Expression attribute)
	{
		//	var var0 = attribute.Before<input, output>(inputSource)
		//	if (var0.stop == true)
		//		return var0.stopWithValue;

		var invoke = MkWrapAttribute_Before.MakeGenericMethod(method.Input, method.Result);

		var invocation = Expression.Call(attribute, invoke, method.Named(MahoganyConstants.MOUNT_VAR), method.Named(MahoganyConstants.INPUT_VAR));

		var setter = method.AsVariable(invocation, out var result);

		var guard =
			Expression.Block(
				setter,
			Expression.IfThen(
			Expression.IsTrue(Expression.Field(result, WRAPRESULT_STOP)),
			Expression.Return(method.HaltTarget, Expression.Field(result, WRAPRESULT_VALUE))
			));

		return Expression.Block(
			guard,
			result);
	}

	/// <summary>
	/// Create an "after" filter which can modify the output.
	/// </summary>
	/// <param name="attribute">An expression which represents the MkWrapAttribute in use for this ececution.</param>
	/// <param name="outputSource"></param>
	/// <param name="output"></param>
	/// <returns></returns>
	internal static Expression After(Expression attribute, Expression mountSource, Expression outputSource, Type output)
	{
		//	attribute.After<output>(outputSource);

		var method = MkWrapAttribute_After.MakeGenericMethod(output);

		return Expression.Call(attribute, method, mountSource,  outputSource);
	}

	internal static Expression After(MahoganyMethod method, Expression attribute)
	{
		return After(attribute,  method.Named(MahoganyConstants.MOUNT_VAR), method.Named(MahoganyConstants.OUTPUT_VAR), method.Result);
	}


}
