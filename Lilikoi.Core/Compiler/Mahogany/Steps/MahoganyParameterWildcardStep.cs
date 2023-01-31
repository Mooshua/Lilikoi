//       ========================
//       Lilikoi.Core::MahoganyParameterWildcardStep.cs
//       Distributed under the MIT License.
//
// ->    Created: 03.01.2023
// ->    Bumped: 03.01.2023
//
// ->    Purpose:
//
//
//       ========================
using System;
using System.Linq.Expressions;
using System.Reflection;

using Lilikoi.Core.Attributes.Builders;
using Lilikoi.Core.Compiler.Mahogany.Generator;

namespace Lilikoi.Core.Compiler.Mahogany.Steps;

public class MahoganyParameterWildcardStep
{
	public MahoganyParameterWildcardStep(MahoganyMethod method, Type wildcard, LkParameterBuilderAttribute builder)
	{
		Method = method;
		Wildcard = wildcard;
		Builder = builder;
	}

	public MahoganyMethod Method { get; }

	public Type Wildcard { get; }

	public LkParameterBuilderAttribute Builder { get; }

	public Expression Generate()
	{
		var instance =
			Method.AsHoistedVariable(ParameterGenerator.Builder(Builder, Method.Mount));

		return ParameterGenerator.InjectWildcard(Method, instance, Wildcard);
	}
}
