//       ========================
//       Lilikoi::LilikoiMethod.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
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
			Implementation = new MahoganyMethod(method)
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
		Implementation.Wildcards.Add(typeof(TInput), Implementation.Named(MahoganyConstants.INPUT_VAR));

		return this;
	}

	public LilikoiMethod Output<TOutput>()
	{
		Implementation.Result = typeof(TOutput);
		Implementation.NamedVariables.Add(MahoganyConstants.OUTPUT_VAR, Expression.Parameter(typeof(TOutput), MahoganyConstants.OUTPUT_VAR));

		if (!Implementation.Result.IsAssignableFrom(Implementation.Return))
			throw new InvalidCastException($"Cannot cast to .Output<T>() result of {typeof(TOutput).FullName} from container host return of {Implementation.Return.FullName}");

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
