//       ========================
//       Lilikoi::CommonGenerator.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using System.Linq.Expressions;

#endregion

namespace Lilikoi.Compiler.Mahogany.Generator;

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