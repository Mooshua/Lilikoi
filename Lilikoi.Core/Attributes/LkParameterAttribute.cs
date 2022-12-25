//       ========================
//       Lilikoi.Core::LkParameterAttribute.cs
//       Distributed under the MIT License.
//
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Core.Attributes.Builders;
using Lilikoi.Core.Context;

namespace Lilikoi.Core.Attributes;

public abstract class LkParameterAttribute : LkParameterBuilderAttribute
{

	public sealed override LkParameterAttribute Build(Mount mount)
	{
		return this.MemberwiseClone() as LkParameterAttribute;
	}

	/// <summary>
	/// A function which accepts a context and the input and returns a type
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
