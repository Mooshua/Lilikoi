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

using Lilikoi.Core.Context;

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
	public sealed override TInjectable Inject<TInjectable>(Mount context)
		where TInjectable : class
	{
		var input = Inject(context);

		var casted = input as TInjectable;

		if (casted is null)
			throw new InvalidCastException($"Cannot cast {input.GetType().Name} to {typeof(TInjectable).Name}. (Result of 'as' is null)");

		return casted;
	}

	public override void Deject<TInjectable>(Mount context, TInjectable injected)
		where TInjectable : class
	{
		var casted = injected as TInject;

		if (casted is null)
			throw new InvalidCastException($"Cannot cast {injected.GetType().Name} to {typeof(TInject).Name}. (Result of 'as' is null)");

		Deject(context, casted);
	}

	public sealed override bool IsInjectable<TInjectable>()
	{
		return typeof(TInjectable) == typeof(TInject);
	}

	#region Abstract

	public abstract TInject Inject(Mount context);

	public virtual void Deject(Mount context, TInject injected)
	{
		return;
	}

	#endregion
}
