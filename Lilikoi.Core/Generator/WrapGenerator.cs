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
namespace Lilikoi.Core.Generator;
/*
internal static class WrapGenerator
{

	internal static MethodInfo MkWrapBuilderAttribute_Build = typeof(MkWrapBuilderAttribute).GetMethod("Build");

	public static MethodInfo MkWrapAttribute_Before = typeof(MkWrapAttribute).GetMethod("Before");
	public static MethodInfo MkWrapAttribute_After = typeof(MkWrapAttribute).GetMethod("After");

	public const string WRAPRESULT_STOP = nameof(MkWrapAttribute.WrapResult<MkWrapAttribute>.stop);
	public const string WRAPRESULT_VALUE = nameof(MkWrapAttribute.WrapResult<MkWrapAttribute>.stopWithValue);

	/// <summary>
	/// Create an expression which creates an MkWrapAttribute.
	/// </summary>
	/// <param name="builderAttribute"></param>
	/// <returns></returns>
	internal static Expression Builder(MkWrapBuilderAttribute builderAttribute)
	{
		return Expression.Call(Expression.Constant(builderAttribute), MkWrapBuilderAttribute_Build);
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
	internal static Expression Before(ContainerBuilder builder, Expression attribute, Expression inputSource, Type input, Type output)
	{
		//	var var0 = attribute.Before<input, output>(inputSource)
		//	if (var0.stop == true)
		//		return var0.stopWithValue;

		var method = MkWrapAttribute_Before.MakeGenericMethod(input, output);

		var invocation = Expression.Call(attribute, method, inputSource);

		var guard = Expression.IfThen(
			Expression.IsTrue(Expression.Field(invocation, WRAPRESULT_STOP)),
			Expression.Goto(builder.Exit, Expression.Field(invocation, WRAPRESULT_VALUE))
			);

		return Expression.Block(
			invocation,
			guard,
			invocation);
	}

	internal static Expression Before(ContainerBuilder builder, Expression attribute, Expression inputSource)
	{
		return Before(builder, attribute, inputSource, builder.Input, builder.Output);
	}

	/// <summary>
	/// Create an "after" filter which can modify the output.
	/// </summary>
	/// <param name="attribute">An expression which represents the MkWrapAttribute in use for this ececution.</param>
	/// <param name="outputSource"></param>
	/// <param name="output"></param>
	/// <returns></returns>
	internal static Expression After(Expression attribute, Expression outputSource, Type output)
	{
		//	attribute.After<output>(outputSource);

		var method = MkWrapAttribute_After.MakeGenericMethod(output);

		return Expression.Call(attribute, method, outputSource);
	}

	internal static Expression After(ContainerBuilder builder, Expression attribute, Expression outputSource)
	{
		return After(attribute, outputSource, builder.Output);
	}
}
*/