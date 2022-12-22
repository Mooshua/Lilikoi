//       ========================
//       Miliko.Core::MahoganyBlockStack.cs
//       Distributed under the MIT License.
//
// ->    Created: 06.12.2022
// ->    Bumped: 06.12.2022
//
// ->    Purpose:
//
//
//       ========================
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Miliko.API.Builder.Mahogany;

public class MahoganyBlockStack
{
	public MahoganyMethod Method { get; set; }

	protected Stack<Expression> Blocks { get; set; } = new Stack<Expression>();

	public void Push(Expression enter, Expression exit)
	{
		Method.Append(enter);
		Blocks.Push(exit);
	}

	public void Apex(Expression apex)
	{
		Method.Append(apex);

		while (Blocks.Count != 0)
		{
			Method.Append(Blocks.Pop());
		}
	}

}
