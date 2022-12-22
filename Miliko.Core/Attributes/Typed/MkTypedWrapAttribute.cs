//       ========================
//       Miliko.API::MkTypedWrapAttribute.cs
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
/// This type supports wrap inputs and outputs that supersede the requested class.
/// </summary>
/// <typeparam name="TIn"></typeparam>
/// <typeparam name="TOut"></typeparam>
[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public abstract class MkTypedWrapAttribute<TIn, TOut> : MkWrapAttribute
	where TIn : class
	where TOut: class
{

	#region Abstract
	public abstract WrapResult<TOut> Before(ref TIn input);

	public abstract void After(ref TOut output);

	#endregion

	public sealed override WrapResult<TOutput> Before<TInput, TOutput>( TInput input)
	{
		var casted = input as TIn;

		if (casted is null)
			throw new InvalidCastException($"Cannot cast {input.GetType().Name} to {typeof(TIn).Name}. (Result of 'as' is null)");

		return Before(ref casted).Cast<TOutput>();
	}

	public sealed override void After<TOutput>( TOutput output)
	{
		var casted = output as TOut;

		if (casted is null)
			throw new InvalidCastException($"Cannot cast {output.GetType().Name} to {typeof(TOut).Name}. (Result of 'as' is null)");

		After(ref casted);
	}

	public sealed override bool IsAcceptableInput<TInput>() => typeof(TIn).IsAssignableFrom( typeof(TInput) );

	public sealed override bool IsAcceptableOutput<TOutput>() => typeof(TOut).IsAssignableFrom( typeof(TOutput) );
}
