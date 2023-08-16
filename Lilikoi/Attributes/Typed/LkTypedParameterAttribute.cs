//       ========================
//       Lilikoi::LkTypedParameterAttribute.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Context;

#endregion

namespace Lilikoi.Attributes.Typed;

public abstract class LkTypedParameterAttribute<TOutput, TInput> : LkParameterAttribute
	where TInput : class
	where TOutput : class
{
	public override bool IsInjectable<TParameter, TIn>(Mount mount)
	{
		return typeof(TParameter).IsAssignableFrom(typeof(TOutput)) &&
		       typeof(TInput).IsAssignableFrom(typeof(TIn));
	}

	public override TParameter Inject<TParameter, TIn>(Mount context, TIn input)
	{
		var inputCasted = input as TInput;

		if (inputCasted is null)
			throw new InvalidCastException($"Cannot cast {typeof(TIn).Name} to {typeof(TInput).Name}. (Result of 'as' is null)");

		var result = Inject(context, inputCasted);

		var output = result as TParameter;

		if (output is null)
			throw new InvalidCastException($"Cannot cast {typeof(TOutput).Name} to {typeof(TParameter).Name}. (Result of 'as' is null)");

		return output;
	}

	public abstract TOutput Inject(Mount context, TInput input);
}