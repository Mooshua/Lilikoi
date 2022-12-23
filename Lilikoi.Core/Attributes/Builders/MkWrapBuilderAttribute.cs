//       ========================
//       Lilikoi.Core::MkWrapBuilderAttribute.cs
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

#endregion

namespace Lilikoi.Core.Attributes.Builders;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public abstract class MkWrapBuilderAttribute : Attribute
{
	public abstract MkWrapAttribute Build();

	/// <summary>
	///     Whether or not this input type will be accepted
	/// </summary>
	/// <typeparam name="TInput"></typeparam>
	/// <returns></returns>
	public abstract bool IsAcceptableInput<TInput>();

	/// <summary>
	///     Whether or not this output type will be accepted.
	///     Used during container instantiation to guard types.
	/// </summary>
	/// <typeparam name="TOutput"></typeparam>
	/// <returns></returns>
	public abstract bool IsAcceptableOutput<TOutput>();
}