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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using Lilikoi.Core.Attributes.Builders;
using Lilikoi.Core.Builder.Mahogany.Generator;

namespace Lilikoi.Core.Builder.Mahogany.Steps;

public class MahoganyWrapStep
{
	public MahoganyWrapStep(MahoganyMethod method, LkWrapBuilderAttribute builder, Type actual)
	{
		Method = method;
		Builder = builder;
		Actual = actual;
	}

	public Type Actual { get; set; }

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
