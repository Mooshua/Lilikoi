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

	public TOut Run<THost, TIn, TOut>(THost host, TIn input)
	{
		if (Memoized is null)
			Memoized = Compile<THost, TIn, TOut>();

		return (Memoized as Func<THost, TIn, TOut>)(host, input);
	}

	public Func<THost, TIn, TOut> Compile<THost, TIn, TOut>()
	{
		return Body.Compile(false) as Func<THost, TIn, TOut>;
	}
}