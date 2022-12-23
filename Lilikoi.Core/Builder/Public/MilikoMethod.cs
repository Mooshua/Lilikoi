//       ========================
//       Lilikoi.Core::MilikoMethod.cs
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

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using Lilikoi.Core.Builder.Mahogany;

#endregion

namespace Lilikoi.Core.Builder.Public;

public class MilikoMethod
{
	internal MahoganyMethod Implementation { get; set; }

	public static MilikoMethod FromMethodInfo(MethodInfo method)
	{
		return new MilikoMethod()
		{
			Implementation = new MahoganyMethod()
			{
				Parameters = method.GetParameters().Select(x => x.ParameterType).ToList(),
				Return = method.ReturnType,
				Entry = method,
				Host = method.DeclaringType,
				NamedVariables = new Dictionary<string, ParameterExpression>()
				{
					{ MahoganyConstants.HOST_VAR, Expression.Parameter(method.DeclaringType, MahoganyConstants.HOST_VAR) }
				}
			}
		};
	}

	/// <summary>
	///     Add an input sequentially to this container
	/// </summary>
	/// <typeparam name="TInput"></typeparam>
	/// <returns></returns>
	public MilikoMethod Input<TInput>()
	{
		Implementation.Input = typeof(TInput);
		Implementation.NamedVariables.Add(MahoganyConstants.INPUT_VAR, Expression.Parameter(typeof(TInput), MahoganyConstants.INPUT_VAR));

		return this;
	}

	public MilikoMethod Output<TOutput>()
	{
		Implementation.Result = typeof(TOutput);
		Implementation.NamedVariables.Add(MahoganyConstants.OUTPUT_VAR, Expression.Parameter(typeof(TOutput), MahoganyConstants.OUTPUT_VAR));


		return this;
	}

	public MilikoCompiler Build()
	{
		var internalMethod = Implementation;

		return new MilikoCompiler()
		{
			Internal = new MahoganyCompiler()
			{
				Method = internalMethod,
				Stack = new MahoganyBlockStack
				{
					Method = internalMethod
				}
			}
		};
	}
}