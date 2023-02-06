//       ========================
//       Lilikoi::LkWrapAttribute.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Attributes.Builders;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public abstract class LkWrapAttribute : LkWrapBuilderAttribute
{
	/// <summary>
	///     This is a static definition, not a dynamic one.
	///     Do not allow inherited classes to mess with this.
	/// </summary>
	/// <param name="mount"></param>
	/// <returns></returns>
	public sealed override LkWrapAttribute Build(Mount mount)
	{
		return MemberwiseClone() as LkWrapAttribute;
	}

	public abstract WrapResult<TOutput> Before<TInput, TOutput>(Mount mount, ref TInput input)
		where TInput : class
		where TOutput : class;

	public abstract void After<TOutput>(Mount mount, ref TOutput output)
		where TOutput : class;


	public struct WrapResult<TUnderlying>
		where TUnderlying : class
	{
		internal bool stop = false;
		internal TUnderlying? stopWithValue = default;

		public WrapResult()
		{
		}

		public WrapResult(bool shouldStop, TUnderlying? value = null)
		{
			stop = shouldStop;
			stopWithValue = value;
		}

		public static WrapResult<TUnderlying> Continue()
		{
			return new WrapResult<TUnderlying>();
		}

		public static WrapResult<TUnderlying> Stop(TUnderlying value)
		{
			return new WrapResult<TUnderlying>
			{
				stop = true,
				stopWithValue = value
			};
		}

		public WrapResult<TNew> Cast<TNew>()
			where TNew : class
		{
			return new WrapResult<TNew>
			{
				stop = stop,
				stopWithValue = stopWithValue as TNew
			};
		}
	}
}