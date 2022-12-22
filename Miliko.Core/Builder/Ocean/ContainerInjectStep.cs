//       ========================
//       Miliko.Core::ContainerInjectStep.cs
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
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection;

using Miliko.API.Attributes.Builders;
using Miliko.Attributes.Generator;

namespace Miliko.Attributes.Builder.Ocean;
/*
public class ContainerInjectStep
{
	public MkInjectionBuilderAttribute Builder { get; set; }

	public Expression Source { get; set; }

	public Expression? Value { get; set; } = null;

	public PropertyInfo PropertyInfo { get; set; }

	public ContainerInjectStep(MkInjectionBuilderAttribute builder, PropertyInfo property)
	{
		Builder = builder;
		Source = InjectionGenerator.Builder(builder);
		PropertyInfo = property;
	}

	/// <summary>
	/// Generate a parameter injector
	/// </summary>
	/// <param name="hostSource">An expression that resolves to the object being injected.</param>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public Expression Ante(Expression hostSource)
	{
		if (Value != null)
			throw new Exception("Ante may not be called twice on the same ContainerInjectStep.");

		Value = InjectionGenerator.InjectValueAsProperty(Source, hostSource, PropertyInfo );
		return Value;
	}

	/// <summary>
	/// Deject the value that was injected by this inject step.
	/// </summary>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public Expression Post()
	{
		if (Value == null)
			throw new Exception("Ante must be called once before Post.");

		return InjectionGenerator.DejectValue(Source, Value);
	}
}
*/
