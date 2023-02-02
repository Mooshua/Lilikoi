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

using Lilikoi.Attributes.Builders;
using Lilikoi.Context;

namespace Lilikoi.Compiler.Public;

public class LilikoiMutator : Mount
{
	internal LilikoiMutator(Mount self, LilikoiCompiler compiler) : base(self)
	{
		Compiler = compiler;
	}

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
	public LilikoiMutator Wildcard<TType>(LkParameterBuilderAttribute value)
	{
		Compiler.ImplicitWildcards.Add( (value, typeof(TType)) );
		return this;
	}

	/// <summary>
	/// Add a parameter wildcard to this container.
	/// If a null value is provided, the default constructor is used.
	/// </summary>
	/// <param name="value"></param>
	/// <typeparam name="TParameter"></typeparam>
	/// <returns></returns>
	public LilikoiMutator Wildcard<TType, TParameter>(TParameter value = null)
	where TParameter: LkParameterBuilderAttribute, new()
	{
		if (value is null)
			value = new TParameter();

		Compiler.ImplicitWildcards.Add( (value, typeof(TType)) );
		return this;
	}

	/// <summary>
	/// Get the parameter type of a function by the parameter number
	/// Used for type routing
	/// </summary>
	/// <param name="paramNum"></param>
	/// <returns></returns>
	public Type? Parameter(int paramNum = 0)
	{
		var parameters = Compiler.Internal.Method.Parameters;

		if (parameters.Count <= paramNum)
			return null;

		return parameters[paramNum];
	}
}
