//       ========================
//       Lilikoi.Core::MahoganyWrapStep.cs
//       Distributed under the MIT License.
//
// ->    Created: 24.12.2022
// ->    Bumped: 24.12.2022
//
// ->    Purpose:
//
//
//       ========================

using System;
using System.Linq.Expressions;

using Lilikoi.Core.Attributes.Builders;
using Lilikoi.Core.Compiler.Mahogany.Generator;

namespace Lilikoi.Core.Compiler.Mahogany.Steps;

public class MahoganyWrapStep
{
	public MahoganyWrapStep(MahoganyMethod method, LkWrapBuilderAttribute builder)
	{
		Method = method;
		Builder = builder;
	}

	public MahoganyMethod Method { get; set; }

	public LkWrapBuilderAttribute Builder { get; set; }

	public (Expression, Expression) Generate()
	{
		var setter =
			Method.AsVariable(
				 WrapGenerator.Builder(Builder, Method.Mount), out var instance);

		//var injects = MahoganyCompiler.InjectStepBuilder(Actual, Method);

		//var pairs = injects.Select(x => x.GenerateFor(instance))
		//	.DefaultIfEmpty( (Expression.Empty(), Expression.Empty()));

		//var enterInj = Expression.Block( pairs.Select(x => x.Item1) );
		//var exitInj = Expression.Block( pairs.Select(x => x.Item2) );

		var entry =
			Expression.Block(
				setter,
				//enterInj,
				WrapGenerator.Before(Method, instance)
			);
		var exit =
			Expression.Block(
				//exitInj,
				WrapGenerator.After(Method, instance));


		return (entry, exit);
	}
}
