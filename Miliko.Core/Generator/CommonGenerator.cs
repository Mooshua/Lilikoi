//       ========================
//       Miliko.Core::CommonGenerator.cs
//       Distributed under the MIT License.
//
// ->    Created: 05.12.2022
// ->    Bumped: 05.12.2022
//
// ->    Purpose:
//
//
//       ========================
using System;
using System.Linq.Expressions;

namespace Miliko.Attributes.Generator;

public static class CommonGenerator
{
	public static Expression GuardAgainstNull(Expression source, Exception throwable)
	{
		//	if (source == null)
		//		throw throwable;
		//	return source;



		return
			Expression.IfThen(
				Expression.Equal(source, Expression.Constant(null, source.Type)),
				Expression.Throw(Expression.Constant(throwable))
				);
	}

	public static Expression ToVariable(Expression input, out ParameterExpression variable)
	{
		//	var var0 = input()
		//	return var0

		variable = Expression.Variable(input.Type, "temp");

		var block =
			Expression.Assign(variable, input);

		return block;
	}

}
