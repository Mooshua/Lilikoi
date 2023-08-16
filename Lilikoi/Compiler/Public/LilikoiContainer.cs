//       ========================
//       Lilikoi::LilikoiContainer.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using System.Linq.Expressions;

using Lilikoi.Context;

#endregion

namespace Lilikoi.Compiler.Public;

public class LilikoiContainer : Mount
{
	internal LilikoiContainer(Mount self, LambdaExpression body) : base(self)
	{
		Body = body;
	}

	private LambdaExpression Body { get; set; }

	private Delegate Memoized { get; set; }

	public TOut Run<TIn, TOut>(TIn input)
	{
		if (Memoized is null)
			Memoized = Compile<TIn, TOut>();

		return (Memoized as Func<TIn, TOut>)(input);
	}

	public Func<TIn, TOut> Compile<TIn, TOut>()
	{
		return Body.Compile(false) as Func<TIn, TOut>;
	}
}
