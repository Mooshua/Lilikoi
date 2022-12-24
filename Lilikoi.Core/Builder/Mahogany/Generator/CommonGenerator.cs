//       ========================
//       Lilikoi.Core::CommonGenerator.cs
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
using System.Linq.Expressions;

#endregion

namespace Lilikoi.Core.Builder.Mahogany.Generator;

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

	public static Expression ToVariable(Expression input, out ParameterExpression variable, string name = "temp")
	{
		//	var var0 = input()
		//	return var0

		variable = Expression.Variable(input.Type, name);

		var block =
			Expression.Assign(variable, input);

		return block;
	}
}
