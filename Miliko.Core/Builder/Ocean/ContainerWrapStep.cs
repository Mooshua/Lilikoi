//       ========================
//       Miliko.Core::ContainerWrapStep.cs
//       Distributed under the MIT License.
//
// ->    Created: 05.12.2022
// ->    Bumped: 05.12.2022
//
// ->    Purpose:
//
//
//       ========================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Miliko.API.Attributes.Builders;
using Miliko.API.Builder;
using Miliko.Attributes.Generator;

namespace Miliko.Attributes.Builder.Ocean;
/*
internal class ContainerWrapStep
{
	public MkWrapBuilderAttribute Builder { get; set; }

	public Expression Source { get; set; }

	public Expression? Value { get; set; } = null;


	public ContainerWrapStep(MkWrapBuilderAttribute builder)
	{
		Source = WrapGenerator.Builder(builder);
	}

	public Expression Ante(ContainerBuilder builder, Expression input)
	{
		if (Value != null)
			throw new Exception("Ante may not be called twice on the same ContainerInjectStep.");

		Value = WrapGenerator.Before(builder, Source, input);
		return Value;
	}

	public Expression Post(ContainerBuilder builder, Expression output)
	{
		if (Value == null)
			throw new Exception("Ante must be called once before Post.");

		return WrapGenerator.After(builder, Source, output);
	}
}
*/
