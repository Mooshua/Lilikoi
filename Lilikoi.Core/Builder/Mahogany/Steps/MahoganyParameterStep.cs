//       ========================
//       Lilikoi.Core::MahoganyParameterStep.cs
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
using System.Reflection;

using Lilikoi.Core.Attributes.Builders;
using Lilikoi.Core.Builder.Mahogany.Generator;

namespace Lilikoi.Core.Builder.Mahogany;

public class MahoganyParameterStep
{
	public MahoganyParameterStep(MahoganyMethod method, ParameterInfo parameter, LkParameterBuilderAttribute builder)
	{
		Method = method;
		ParameterInfo = parameter;
		Builder = builder;
	}

	public MahoganyMethod Method { get; set; }

	public ParameterInfo ParameterInfo { get; set; }

	public LkParameterBuilderAttribute Builder { get; set; }

	public Expression Generate()
	{
		var instance =
			Method.AsHoistedVariable(ParameterGenerator.Builder(Builder));

		return ParameterGenerator.Inject(Method, instance, ParameterInfo);
	}
}
