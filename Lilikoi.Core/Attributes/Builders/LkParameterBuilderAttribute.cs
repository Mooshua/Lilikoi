//       ========================
//       Lilikoi.Core::LkParameterBuilderAttribute.cs
//       Distributed under the MIT License.
//
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
//
// ->    Purpose:
//
//
//       ========================
using System;

namespace Lilikoi.Core.Attributes.Builders;

[AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
public abstract class LkParameterBuilderAttribute : Attribute
{

	/// <summary>
	/// A function which returns the parameter injector to use for this parameter.
	/// </summary>
	/// <returns></returns>
	public abstract LkParameterAttribute Build();

	/// <summary>
	/// Whether or not this parameter can handle an input of <see cref="TInput"/> and provide an output of <see cref="TParameter"/>.
	/// </summary>
	/// <typeparam name="TParameter"></typeparam>
	/// <typeparam name="TInput"></typeparam>
	/// <returns></returns>
	public abstract bool IsInjectable<TParameter, TInput>()
		where TInput : class;

}
