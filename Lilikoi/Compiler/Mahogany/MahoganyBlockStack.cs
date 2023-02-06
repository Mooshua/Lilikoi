//       ========================
//       Lilikoi::MahoganyBlockStack.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using System.Linq.Expressions;

#endregion

namespace Lilikoi.Compiler.Mahogany;

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