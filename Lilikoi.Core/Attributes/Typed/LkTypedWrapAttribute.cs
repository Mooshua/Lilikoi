//       ========================
//       Lilikoi.Core::MkTypedWrapAttribute.cs
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

using Lilikoi.Core.Context;

#endregion

namespace Lilikoi.Core.Attributes.Typed;

/// <summary>
///     A wrap attribute with static generic typing instead of per-invocation generic typing.
///     This type supports wrap inputs and outputs that supersede the requested class.
/// </summary>
/// <typeparam name="TIn"></typeparam>
/// <typeparam name="TOut"></typeparam>
[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public abstract class LkTypedWrapAttribute<TIn, TOut> : LkWrapAttribute
	where TIn : class
	where TOut : class
{
	public sealed override WrapResult<TOutput> Before<TInput, TOutput>(Mount mount, ref TInput input)
	{

		var casted = input as TIn;

		if (casted is null)
			throw new InvalidCastException($"Cannot cast {input.GetType().Name} to {typeof(TIn).Name}. (Result of 'as' is null)");

		return Before(mount, ref casted).Cast<TOutput>();
	}

	public sealed override void After<TOutput>(Mount mount, ref TOutput output)
	{
		var casted = output as TOut;

		if (casted is null)
			throw new InvalidCastException($"Cannot cast {output.GetType().Name} to {typeof(TOut).Name}. (Result of 'as' is null)");

		After(mount, ref casted);
	}

	public sealed override bool IsWrappable<TInput, TOutput>(Mount mount)
	{
		return typeof(TIn).IsAssignableFrom(typeof(TInput)) &&
			typeof(TOut).IsAssignableFrom(typeof(TOutput));
	}

	#region Abstract

	public abstract WrapResult<TOut> Before(Mount mount, ref TIn input);

	public abstract void After(Mount mount, ref TOut output);

	#endregion
}
