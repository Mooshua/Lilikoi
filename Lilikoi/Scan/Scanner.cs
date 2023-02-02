//       ========================
//       Lilikoi.Core::Scanner.cs
//
// ->    Created: 31.01.2023
// ->    Bumped: 31.01.2023
//
// ->    Purpose:
//
//
//       ========================
using System.Collections.Concurrent;
using System.Reflection;

using Lilikoi.Attributes.Builders;
using Lilikoi.Compiler.Public;
using Lilikoi.Context;

namespace Lilikoi.Scan;

public static class Scanner
{

	/// <summary>
	/// Scan the provided assembly for all Lilikoi Containers matching the user context and return them
	/// Use the provided mount as the global mount
	/// </summary>
	/// <param name="context">A user-supplied context potentially consumed by a target</param>
	/// <param name="assembly"></param>
	/// <param name="mount"></param>
	/// <typeparam name="TUserContext"></typeparam>
	/// <typeparam name="TInput"></typeparam>
	/// <typeparam name="TOutput"></typeparam>
	/// <returns></returns>
	public static List<LilikoiContainer> Scan<TUserContext, TInput, TOutput>(TUserContext context, Assembly assembly, Mount mount)
		where TUserContext : Mount
	{
		return Scan<TUserContext, TInput, TOutput>(context, assembly, () => mount);
	}

	/// <summary>
	/// Scan the provided assembly for all Lilikoi Containers matching the user context and return them
	/// Use a provided function to supply the Mount for each container.
	/// </summary>
	/// <param name="context">A user-supplied context potentially consumed by a target</param>
	/// <param name="assembly"></param>
	/// <param name="sourceMount"></param>
	/// <typeparam name="TUserContext"></typeparam>
	/// <typeparam name="TInput"></typeparam>
	/// <typeparam name="TOutput"></typeparam>
	/// <returns></returns>
	public static List<LilikoiContainer> Scan<TUserContext, TInput, TOutput>(TUserContext context, Assembly assembly, Func<Mount> sourceMount)
		where TUserContext : Mount
	{
		List<LilikoiContainer> containers = new List<LilikoiContainer>();

		foreach (Type type in assembly.GetTypes())
		{
			foreach (MethodInfo methodInfo in type.GetMethods())
			{
				foreach (LkTargetBuilderAttribute attribute in methodInfo.GetCustomAttributes()
					         .Where(attribute => attribute
						         .GetType()
						         .IsSubclassOf(typeof(LkTargetBuilderAttribute))))
				{
					if (attribute.IsTargetable<TUserContext>())
					{
						var mount = sourceMount();

						//	This method is targeted!
						//	Begin container creation
						var compiler = LilikoiMethod.FromMethodInfo(methodInfo)
							.Mount(mount)
							.Input<TInput>()
							.Output<TOutput>()
							.Build();

						var built = attribute.Build(mount);
						built.Target(context, compiler.Mutator());

						containers.Add( compiler.Finish() );
					}
				}
			}
		}

		return containers;
	}
}
