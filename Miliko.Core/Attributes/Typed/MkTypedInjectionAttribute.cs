//       ========================
//       Miliko.API::MkTypedInjectionAttribute.cs
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

namespace Miliko.API.Attributes;

/// <summary>
/// A wrap attribute with static generic typing instead of per-invocation generic typing.
///
/// This type requires that the injected type be exact, and not be inherited or a parent of the requested type.
/// </summary>
/// <typeparam name="TIn"></typeparam>
/// <typeparam name="TOut"></typeparam>
public abstract class MkTypedInjectionAttribute<TInject> : MkInjectionAttribute
	where TInject : class
{
	#region Abstract

	public abstract TInject Inject();

	public abstract void Deject(TInject injected);

	#endregion


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

	public sealed override bool IsInjectable<TInjectable>() => typeof(TInjectable) == typeof(TInject);
}
