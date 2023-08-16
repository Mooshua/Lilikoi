//       ========================
//       Lilikoi::LkWrapBuilderAttribute.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Context;

#endregion

namespace Lilikoi.Attributes.Builders;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public abstract class LkWrapBuilderAttribute : Attribute
{
	public abstract LkWrapAttribute Build(Mount mount);

	/// <summary>
	///     Whether or not this input type and output type will be accepted
	/// </summary>
	/// <typeparam name="TInput"></typeparam>
	/// <typeparam name="TOutput"></typeparam>
	/// <returns></returns>
	public abstract bool IsWrappable<TInput, TOutput>(Mount mount);
}