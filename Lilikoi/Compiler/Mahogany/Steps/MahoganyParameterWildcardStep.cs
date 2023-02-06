//       ========================
//       Lilikoi::MahoganyParameterWildcardStep.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 03.01.2023
// ->    Bumped: 06.02.2023
//       ========================
#region

using System.Linq.Expressions;

using Lilikoi.Attributes.Builders;
using Lilikoi.Compiler.Mahogany.Generator;

#endregion

namespace Lilikoi.Compiler.Mahogany.Steps;

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