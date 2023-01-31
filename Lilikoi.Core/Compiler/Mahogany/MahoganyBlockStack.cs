//       ========================
//       Lilikoi.Core::MahoganyBlockStack.cs
//       Distributed under the MIT License.
//
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
//
// ->    Purpose:
//		 Block stack to ensure the pyramid is executed in the same order climbing both up and down.
//
//
//       ========================
#region

using System.Collections.Generic;
using System.Linq.Expressions;

#endregion

namespace Lilikoi.Core.Compiler.Mahogany;

public class MahoganyBlockStack
{
	public MahoganyMethod Method { get; set; }

	protected Stack<Expression> Blocks { get; set; } = new();

	public void Push(Expression enter, Expression exit)
	{
		Method.Append(enter);
		Blocks.Push(exit);
	}

	public void Apex(Expression apex)
	{
		Method.Append(apex);

		while (Blocks.Count != 0)
			Method.Append(Blocks.Pop());
	}
}
