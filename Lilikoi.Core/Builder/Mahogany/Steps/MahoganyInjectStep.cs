//       ========================
//       Lilikoi.Core::MahoganyInjectStep.cs
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

using System.Linq.Expressions;
using System.Reflection;

using Lilikoi.Core.Attributes.Builders;
using Lilikoi.Core.Builder.Mahogany.Generator;

#endregion

namespace Lilikoi.Core.Builder.Mahogany.Steps;

public class MahoganyInjectStep
{
	public MahoganyInjectStep(MahoganyMethod method, PropertyInfo propertyInfo, LkInjectionBuilderAttribute builder)
	{
		Method = method;
		PropertyInfo = propertyInfo;
		Builder = builder;
	}

	public MahoganyMethod Method { get; set; }

	public PropertyInfo PropertyInfo { get; set; }

	public LkInjectionBuilderAttribute Builder { get; set; }

	public (Expression, Expression) Generate()
	{
		var instance =
			Method.AsHoistedVariable(InjectionGenerator.Builder(Builder));

		var entry =
			InjectionGenerator.InjectValueAsProperty(Method, instance, Method.Named(MahoganyConstants.HOST_VAR), PropertyInfo);

		var exit =
			InjectionGenerator.DejectValueAsProperty(Method, instance, Method.Named(MahoganyConstants.HOST_VAR), PropertyInfo);

		return (entry, exit);
	}

	public (Expression, Expression) GenerateFor(ParameterExpression target)
	{
		var instance =
			Method.AsHoistedVariable(InjectionGenerator.Builder(Builder));

		var entry =
			InjectionGenerator.InjectValueAsProperty(Method, instance, target, PropertyInfo);

		var exit =
			InjectionGenerator.DejectValueAsProperty(Method, instance, target, PropertyInfo);

		return (entry, exit);
	}
}
