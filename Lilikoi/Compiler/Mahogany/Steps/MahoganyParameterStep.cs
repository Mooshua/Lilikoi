﻿//       ========================
//       Lilikoi::MahoganyParameterStep.cs
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
			Method.AsHoistedVariable(ParameterGenerator.Builder(Builder, Method.Mount));

		return ParameterGenerator.InjectParameter(Method, instance, ParameterInfo);
	}
}