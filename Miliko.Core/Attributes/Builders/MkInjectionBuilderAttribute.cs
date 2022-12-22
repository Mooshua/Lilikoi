//       ========================
//       Miliko.API::MkInjectionBuilderAttribute.cs
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

namespace Miliko.API.Attributes.Builders;

[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
public abstract class MkInjectionBuilderAttribute : Attribute
{

	/// <summary>
	/// Build the MkInjectionAttribute that this builder will replace.
	/// </summary>
	/// <returns></returns>
	public abstract MkInjectionAttribute Build();

	/// <summary>
	/// Whether or not the provided generic type is handled by this injector.
	/// </summary>
	/// <typeparam name="TInjectable"></typeparam>
	/// <returns></returns>
	public abstract bool IsInjectable<TInjectable>()
		where TInjectable : class;

}
