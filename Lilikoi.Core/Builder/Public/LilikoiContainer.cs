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
#region

using System;
using System.Linq.Expressions;

using AgileObjects.ReadableExpressions;

#endregion

namespace Lilikoi.Core.Builder.Public;

public class LilikoiContainer
{
	internal LambdaExpression Body { get; set; }

#if DEBUG

	public TOut Run<THost, TIn, TOut>(THost host, TIn input)
	{
		return (Body.Compile(true) as Func<THost, TIn, TOut>)(host, input);
	}

	public Func<THost, TIn, TOut> Compile<THost, TIn, TOut>() => Body.Compile(true) as Func<THost, TIn, TOut>;

	public override string ToString()
	{
		return ((Expression)Body).ToReadableString(opt => { return opt; });
	}
#endif

#if !DEBUG
	public TOut Run<THost, TIn, TOut>(THost host, TIn input)
	{
		return (Body.Compile(false) as Func<THost, TIn, TOut>)(host, input);
	}

	public Func<THost, TIn, TOut> Compile<THost, TIn, TOut>() => Body.Compile(false) as Func<THost, TIn, TOut>;
#endif
}
