//       ========================
//       Lilikoi::MahoganyInjectStep.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using System.Linq.Expressions;
using System.Reflection;

using Lilikoi.Attributes.Builders;
using Lilikoi.Compiler.Mahogany.Generator;

#endregion

namespace Lilikoi.Compiler.Mahogany.Steps;

public class MahoganyInjectStep
{
	public MahoganyInjectStep(MahoganyMethod method, FieldInfo fieldInfo, LkInjectionBuilderAttribute builder)
	{
		Method = method;
		FieldInfo = fieldInfo;
		Builder = builder;
	}

	public MahoganyMethod Method { get; set; }

	public FieldInfo FieldInfo { get; set; }

	public LkInjectionBuilderAttribute Builder { get; set; }

	public (Expression, Expression) Generate()
	{
		var instance =
			Method.AsHoistedVariable(InjectionGenerator.Builder(Builder, Method.Mount));

		var entry =
			InjectionGenerator.InjectValueAsField(Method, instance, Method.Named(MahoganyConstants.HOST_VAR), FieldInfo);

		var exit =
			InjectionGenerator.DejectValueAsField(Method, instance, Method.Named(MahoganyConstants.HOST_VAR), FieldInfo);

		return (entry, exit);
	}

	public (Expression, Expression) GenerateFor(ParameterExpression target)
	{
		var instance =
			Method.AsHoistedVariable(InjectionGenerator.Builder(Builder, Method.Mount));

		var entry =
			InjectionGenerator.InjectValueAsField(Method, instance, target, FieldInfo);

		var exit =
			InjectionGenerator.DejectValueAsField(Method, instance, target, FieldInfo);

		return (entry, exit);
	}
}