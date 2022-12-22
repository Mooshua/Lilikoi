//       ========================
//       Miliko.Core::MahoganyInjectStep.cs
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
using System.Reflection;

using Miliko.API.Attributes.Builders;
using Miliko.Attributes.Generator;

namespace Miliko.API.Builder.Mahogany;

public class MahoganyInjectStep
{

	public MahoganyMethod Method { get; set; }

	public PropertyInfo PropertyInfo { get; set; }

	public MkInjectionBuilderAttribute Builder { get; set; }

	public MahoganyInjectStep(MahoganyMethod method, PropertyInfo propertyInfo, MkInjectionBuilderAttribute builder)
	{
		Method = method;
		PropertyInfo = propertyInfo;
		Builder = builder;
	}

	public (Expression, Expression) Generate()
	{

		var instance = InjectionGenerator.Builder(Builder);

		var entry =
			InjectionGenerator.InjectValueAsProperty(Method, instance, Method.Named(MahoganyConstants.HOST_VAR), PropertyInfo);

		var exit =
			InjectionGenerator.DejectValueAsProperty(instance, Method.Named(MahoganyConstants.HOST_VAR), PropertyInfo);

		return (entry, exit);

	}

}
