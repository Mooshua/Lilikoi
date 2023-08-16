//       ========================
//       Lilikoi::LilikoiMutator.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 31.01.2023
// ->    Bumped: 06.02.2023
//       ========================
#region

using System.Reflection;

using Lilikoi.Attributes.Builders;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Compiler.Public;

public class LilikoiMutator : Mount
{
	internal LilikoiMutator(Mount self, LilikoiCompiler compiler) : base(self)
	{
		Compiler = compiler;
	}

	internal LilikoiCompiler Compiler { get; set; }

	/// <summary>
	///     Add an implicit wrap to this container
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public LilikoiMutator Implicit(LkWrapBuilderAttribute value)
	{
		Compiler.ImplicitWraps.Add(value);
		return this;
	}

	/// <summary>
	///     Add an implicit wrap to this container.
	///     If a null value is provided, the default constructor is used.
	/// </summary>
	/// <param name="value"></param>
	/// <typeparam name="TWrap"></typeparam>
	/// <returns></returns>
	public LilikoiMutator Implicit<TWrap>(TWrap value = null)
		where TWrap : LkWrapBuilderAttribute, new()
	{
		value ??= new TWrap();

		Compiler.ImplicitWraps.Add(value);
		return this;
	}

	/// <summary>
	///     Add a parameter wildcard to this container.
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public LilikoiMutator Wildcard<TType>(LkParameterBuilderAttribute value)
	{
		Compiler.ImplicitWildcards.Add((value, typeof(TType)));
		return this;
	}

	/// <summary>
	/// Add a parameter wildcard to this container for the provided type
	/// </summary>
	/// <param name="value"></param>
	/// <param name="type"></param>
	/// <returns></returns>
	public LilikoiMutator Wildcard(LkParameterBuilderAttribute value, Type type)
	{
		Compiler.ImplicitWildcards.Add((value, type));
		return this;
	}

	/// <summary>
	///     Add a parameter wildcard to this container.
	///     If a null value is provided, the default constructor is used.
	/// </summary>
	/// <param name="value"></param>
	/// <typeparam name="TParameter"></typeparam>
	/// <typeparam name="TType"></typeparam>
	/// <returns></returns>
	public LilikoiMutator Wildcard<TType, TParameter>(TParameter value = null)
		where TParameter : LkParameterBuilderAttribute, new()
	{
		value ??= new TParameter();

		Compiler.ImplicitWildcards.Add((value, typeof(TType)));
		return this;
	}

	/// <summary>
	///     Add a parameter wildcard to this container for the specified type.
	///     If a null value is provided, the default constructor is used.
	/// </summary>
	/// <param name="value"></param>
	/// <typeparam name="TParameter"></typeparam>
	/// <typeparam name="TType"></typeparam>
	/// <returns></returns>
	public LilikoiMutator Wildcard<TParameter>(Type type, TParameter value = null)
		where TParameter : LkParameterBuilderAttribute, new()
	{
		value ??= new TParameter();

		Compiler.ImplicitWildcards.Add((value, type));
		return this;
	}

	/// <summary>
	///     Get the parameter type of a function by the parameter number
	///     Used for type routing
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

	/// <summary>
	/// Get the number of parameters on the host function
	/// </summary>
	public int Parameters => Compiler.Internal.Method.Parameters.Count;

	/// <summary>
	/// The return type of the underlying function
	/// </summary>
	public Type Result => Compiler.Internal.Method.Return;

	/// <summary>
	/// The entry point that the mutator is applying to
	/// </summary>
	public MethodInfo Method => Compiler.Internal.Method.Entry;

	/// <summary>
	/// The host that the entry point belongs to.
	/// Note that this is the same as Method.DeclaringType
	/// </summary>
	public Type Host => Compiler.Internal.Method.Host;
}
