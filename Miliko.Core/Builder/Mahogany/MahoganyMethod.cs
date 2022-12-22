﻿//       ========================
//       Miliko.Core::MahoganyMethod.cs
//       Distributed under the MIT License.
//
// ->    Created: 06.12.2022
// ->    Bumped: 19.12.2022
//
// ->    Purpose:
//
//
//       ========================
#region

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

using Miliko.Attributes.Generator;

#endregion

namespace Miliko.API.Builder.Mahogany;

public class MahoganyMethod
{
	public List<ParameterExpression> Temporaries = new();

	/// <summary>
	///     A list of parameters that are to be injected into the method, indexed by the parameter they
	///     will be filling.
	/// </summary>
	public Dictionary<ParameterInfo, ParameterExpression> MethodInjects { get; set; } = new();

	public Dictionary<string, ParameterExpression> NamedVariables { get; set; } = new();

	public Dictionary<Type, ParameterExpression> Wildcards { get; set; } = new();

	protected List<Expression> Body { get; set; } = new();

	public ParameterExpression AsUnorderedVariable(Expression input)
	{
		var value = CommonGenerator.ToVariable(input, out var variable);

		Append(value);
		Temporaries.Add(variable);

		return variable;
	}

	public Expression AsVariable(Expression input, out ParameterExpression variable)
	{
		var value = CommonGenerator.ToVariable(input, out variable);

		Temporaries.Add(variable);

		return value;
	}

	public ParameterExpression Named(string name)
	{
		if (!NamedVariables.ContainsKey(name))
			throw new Exception($"named veriable '{name}' not defined.");

		return NamedVariables[name];
	}

	public void Append(Expression block)
	{
		Body.Add(block);
	}

	public LambdaExpression Lambda()
	{
		var func = typeof(Func<,,>).MakeGenericType(Host, Input, Result);

		return Expression.Lambda(
			func,
			Expression.Block(Temporaries.ToArray(), Expression.Block(
				new[]
				{
					//Named(MahoganyConstants.HOST_VAR), Named(MahoganyConstants.INPUT_VAR),
					Named(MahoganyConstants.OUTPUT_VAR)
				},
				Expression.Block(Body), Named(MahoganyConstants.OUTPUT_VAR))),
			Named(MahoganyConstants.HOST_VAR),
			Named(MahoganyConstants.INPUT_VAR));
	}

	#region Containerized

	/// <summary>
	///     Parameters to the containerized method.
	/// </summary>
	public List<Type> Parameters { get; set; }

	/// <summary>
	///     The return value of the containerized method
	/// </summary>
	public Type Return { get; set; }

	/// <summary>
	///     The class that will be injected to accomodate the container.
	/// </summary>
	public Type Host { get; set; }

	public MethodInfo Entry { get; set; }

	#endregion

	#region Container

	/// <summary>
	///     The type provided to the container
	/// </summary>
	public Type Input { get; set; }

	/// <summary>
	///     The return type of the container
	/// </summary>
	public Type Result { get; set; }

	#endregion
}
