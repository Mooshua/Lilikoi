//       ========================
//       Lilikoi::LkInjectionBuilderAttribute.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Context;

#endregion

namespace Lilikoi.Attributes.Builders;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public abstract class LkInjectionBuilderAttribute : Attribute
{
	/// <summary>
	///     Build the MkInjectionAttribute that this builder will replace.
	/// </summary>
	/// <returns></returns>
	public abstract LkInjectionAttribute Build(Mount mount);

	/// <summary>
	///     Whether or not the provided generic type is handled by this injector.
	/// </summary>
	/// <typeparam name="TInjectable"></typeparam>
	/// <returns></returns>
	public abstract bool IsInjectable<TInjectable>(Mount mount)
		where TInjectable : class;
}