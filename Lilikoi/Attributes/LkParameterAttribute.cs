//       ========================
//       Lilikoi::LkParameterAttribute.cs
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

public abstract class LkParameterAttribute : LkParameterBuilderAttribute
{
	public sealed override LkParameterAttribute Build(Mount mount)
	{
		return MemberwiseClone() as LkParameterAttribute;
	}

	/// <summary>
	///     A function which accepts a context and the input and returns a type
	/// </summary>
	/// <param name="context"></param>
	/// <param name="input"></param>
	/// <typeparam name="TParameter">The result type</typeparam>
	/// <typeparam name="TInput">The input type</typeparam>
	/// <returns></returns>
	public abstract TParameter Inject<TParameter, TInput>(Mount context, TInput input)
		where TParameter : class
		where TInput : class;
}