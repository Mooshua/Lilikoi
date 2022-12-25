//       ========================
//       Lilikoi.Core::MkInjectionAttribute.cs
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

using Lilikoi.Core.Attributes.Builders;
using Lilikoi.Core.Context;

#endregion

namespace Lilikoi.Core.Attributes;

/// <summary>
///     An attribute which provides an object to be injected into a container.
/// </summary>
/// <typeparam name="TInjectable"></typeparam>
[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public abstract class LkInjectionAttribute : LkInjectionBuilderAttribute
{
	/// <summary>
	///     This is a static injection attribute, not a dynamic one.
	///     Do not allow inheriting classes to fiddle with this.
	/// </summary>
	/// <returns></returns>
	public sealed override LkInjectionAttribute Build(Mount mount)
	{
		return this.MemberwiseClone() as LkInjectionAttribute;
	}

	/// <summary>
	///     Take parameters and produce an object to be injected into the container
	/// </summary>
	/// <returns></returns>
	public abstract TInjectable Inject<TInjectable>(Mount context)
		where TInjectable : class;


	/// <summary>
	///     Perform any clean-up work to "de-ject" the injectable after the container has finished
	///     executing.
	/// </summary>
	/// <param name="injected"></param>
	public virtual void Deject<TInjectable>(Mount context, TInjectable injected)
		where TInjectable : class
	{
		throw new NotImplementedException();
	}
}
