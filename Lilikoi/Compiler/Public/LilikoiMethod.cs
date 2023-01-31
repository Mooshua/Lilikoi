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

using System.Linq.Expressions;
using System.Reflection;

using Lilikoi.Compiler.Mahogany;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Compiler.Public;

public class LilikoiMethod
{
	internal MahoganyMethod Implementation { get; set; }

	public static LilikoiMethod FromMethodInfo(MethodInfo method)
	{
		return new LilikoiMethod()
		{
			Implementation = new MahoganyMethod()
			{
				Parameters = method.GetParameters().Select(x => x.ParameterType).ToList(),
				Return = method.ReturnType,
				HaltTarget = Expression.Label(method.ReturnType),
				Entry = method,
				Host = method.DeclaringType,
				NamedVariables = new Dictionary<string, ParameterExpression>()
				{
					{ MahoganyConstants.HOST_VAR, Expression.Parameter(method.DeclaringType, MahoganyConstants.HOST_VAR) },
				}
			}
		};
	}

	/// <summary>
	///     Add an input sequentially to this container
	/// </summary>
	/// <typeparam name="TInput"></typeparam>
	/// <returns></returns>
	public LilikoiMethod Input<TInput>()
	{
		Implementation.Input = typeof(TInput);
		Implementation.NamedVariables.Add(MahoganyConstants.INPUT_VAR, Expression.Parameter(typeof(TInput), MahoganyConstants.INPUT_VAR));

		return this;
	}

	public LilikoiMethod Output<TOutput>()
	{
		Implementation.Result = typeof(TOutput);
		Implementation.NamedVariables.Add(MahoganyConstants.OUTPUT_VAR, Expression.Parameter(typeof(TOutput), MahoganyConstants.OUTPUT_VAR));


		return this;
	}

	public LilikoiMethod Mount(Mount mount)
	{
		var mountVar = Implementation.AsHoistedVariable(Expression.Constant(mount));
		Implementation.NamedVariables.Add(MahoganyConstants.MOUNT_VAR, mountVar);
		Implementation.Mount = mount;

		return this;
	}

	public LilikoiCompiler Build()
	{
		var internalMethod = Implementation;

		return new LilikoiCompiler()
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
