//       ========================
//       Lilikoi.Core::LilikoiMutator.cs
//
// ->    Created: 31.01.2023
// ->    Bumped: 31.01.2023
//
// ->    Purpose:
//
//
//       ========================
using System.Collections.Generic;

using Lilikoi.Core.Attributes.Builders;
using Lilikoi.Core.Compiler.Mahogany;

namespace Lilikoi.Core.Compiler.Public;

public class LilikoiMutator
{

	internal LilikoiCompiler Compiler { get; set; }

	/// <summary>
	/// Add an implicit wrap to this container
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public LilikoiMutator Implicit(LkWrapBuilderAttribute value)
	{
		Compiler.ImplicitWraps.Add(value);
		return this;
	}

	/// <summary>
	/// Add an implicit wrap to this container.
	/// If a null value is provided, the default constructor is used.
	/// </summary>
	/// <param name="value"></param>
	/// <typeparam name="TWrap"></typeparam>
	/// <returns></returns>
	public LilikoiMutator Implicit<TWrap>(TWrap value = null)
		where TWrap : LkWrapBuilderAttribute, new()
	{
		if (value is null)
			value = new TWrap();

		Compiler.ImplicitWraps.Add(value);
		return this;
	}

	/// <summary>
	/// Add a parameter wildcard to this container.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public LilikoiMutator Wildcard(LkParameterBuilderAttribute value )
	{
		Compiler.ImplicitWildcards.Add(value);
		return this;
	}

	/// <summary>
	/// Add a parameter wildcard to this container.
	/// If a null value is provided, the default constructor is used.
	/// </summary>
	/// <param name="value"></param>
	/// <typeparam name="TParameter"></typeparam>
	/// <returns></returns>
	public LilikoiMutator Wildcard<TParameter>(TParameter value = null)
	where TParameter: LkParameterBuilderAttribute, new()
	{
		if (value is null)
			value = new TParameter();

		Compiler.ImplicitWildcards.Add(value);
		return this;
	}
}
