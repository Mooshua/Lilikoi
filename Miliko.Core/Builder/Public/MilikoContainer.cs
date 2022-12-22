//       ========================
//       Miliko.Core::MilikoContainer.cs
//       Distributed under the MIT License.
//
// ->    Created: 06.12.2022
// ->    Bumped: 06.12.2022
//
// ->    Purpose:
//
//
//       ========================
using System;
using System.Linq.Expressions;

using AgileObjects.ReadableExpressions;

namespace Miliko.Attributes.Builder.Public;

public class MilikoContainer
{
	internal LambdaExpression Body { get; set; }

	public TOut Run<THost,TIn,TOut>(THost host, TIn input)
	{
		return (Body.Compile(true) as Func<THost, TIn, TOut>)(host, input);
	}

	public override string ToString()
	{
		return ((Expression)Body).ToReadableString(opt =>
		{
			return opt;
		});
	}
}
