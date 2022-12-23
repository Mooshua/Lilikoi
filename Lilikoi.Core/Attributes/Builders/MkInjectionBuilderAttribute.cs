//       ========================
//       Lilikoi.Core::MkInjectionBuilderAttribute.cs
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

using System;

#endregion

namespace Lilikoi.Core.Attributes.Builders;

[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
public abstract class MkInjectionBuilderAttribute : Attribute
{
	/// <summary>
	///     Build the MkInjectionAttribute that this builder will replace.
	/// </summary>
	/// <returns></returns>
	public abstract MkInjectionAttribute Build();

	/// <summary>
	///     Whether or not the provided generic type is handled by this injector.
	/// </summary>
	/// <typeparam name="TInjectable"></typeparam>
	/// <returns></returns>
	public abstract bool IsInjectable<TInjectable>()
		where TInjectable : class;
}
