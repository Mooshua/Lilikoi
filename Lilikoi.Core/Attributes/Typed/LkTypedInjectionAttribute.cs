//       ========================
//       Lilikoi.Core::MkTypedInjectionAttribute.cs
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
using System.Runtime.CompilerServices;

#endregion

namespace Lilikoi.Core.Attributes.Typed;

/// <summary>
///     A wrap attribute with static generic typing instead of per-invocation generic typing.
///     This type requires that the injected type be exact, and not be inherited or a parent of the
///     requested type.
/// </summary>
/// <typeparam name="TIn"></typeparam>
/// <typeparam name="TOut"></typeparam>
public abstract class LkTypedInjectionAttribute<TInject> : LkInjectionAttribute
	where TInject : class
{
	public sealed override TInjectable Inject<TInjectable>()
		where TInjectable : class
	{
		var input = Inject();

		var casted = input as TInjectable;

		if (casted is null)
			throw new InvalidCastException($"Cannot cast {input.GetType().Name} to {typeof(TInjectable).Name}. (Result of 'as' is null)");

		return casted;
	}

	public override void Deject<TInjectable>(TInjectable injected)
		where TInjectable : class
	{
		var casted = injected as TInject;

		if (casted is null)
			throw new InvalidCastException($"Cannot cast {injected.GetType().Name} to {typeof(TInject).Name}. (Result of 'as' is null)");

		Deject(casted);
	}

	public sealed override bool IsInjectable<TInjectable>()
	{
		return typeof(TInjectable) == typeof(TInject);
	}

	#region Abstract

	public abstract TInject Inject();

	public virtual void Deject(TInject injected)
	{
		return;
	}

	#endregion
}
