//       ========================
//       Lilikoi.Core::MilikoContainer.cs
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

using Lilikoi.Context;

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

	public Func<THost, TIn, TOut> Compile<THost, TIn, TOut>() => Body.Compile(false) as Func<THost, TIn, TOut>;
}
