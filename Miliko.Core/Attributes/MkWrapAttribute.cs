//       ========================
//       Miliko.Core::MkWrapAttribute.cs
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
using System.Net;

using Miliko.API.Attributes.Builders;

namespace Miliko.API.Attributes;


[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public abstract class MkWrapAttribute : MkWrapBuilderAttribute
{
	/// <summary>
	/// This is a static definition, not a dynamic one.
	/// Do not allow inherited classes to mess with this.
	/// </summary>
	/// <returns></returns>
	public sealed override MkWrapAttribute Build() => this;

	public abstract WrapResult<TOutput> Before<TInput, TOutput>(TInput input)
		where TInput : class
		where TOutput : class;

	public abstract void After<TOutput>(TOutput output)
		where TOutput : class;



	public struct WrapResult<TUnderlying>
		where TUnderlying : class
	{
		internal bool stop = false;
		internal TUnderlying? stopWithValue = default(TUnderlying);

		public WrapResult()
		{
		}

		public WrapResult(bool shouldStop, TUnderlying? value = null)
		{
			stop = shouldStop;
			stopWithValue = value;
		}

		public static WrapResult<TUnderlying> Continue() => new WrapResult<TUnderlying>();

		public static WrapResult<TUnderlying> Stop(TUnderlying value) => new WrapResult<TUnderlying>()
		{
			stop = true,
			stopWithValue = value
		};

		public WrapResult<TNew> Cast<TNew>()
			where TNew : class
			=> new WrapResult<TNew>()
			{
				stop = this.stop,
				stopWithValue = this.stopWithValue as TNew,
			};
	}

}
