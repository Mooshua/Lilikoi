//       ========================
//       Lilikoi::Scanner.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 31.01.2023
// ->    Bumped: 06.02.2023
//       ========================
#region

using System.Reflection;

using Lilikoi.Attributes.Builders;
using Lilikoi.Compiler.Public;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Scan;

public static class Scanner
{
	/// <summary>
	///     Scan the provided assembly for all Lilikoi Containers matching the user context and return them
	///     Use the provided mount as the global mount
	/// </summary>
	/// <param name="context">A user-supplied context potentially consumed by a target</param>
	/// <param name="assembly"></param>
	/// <param name="mount"></param>
	/// <typeparam name="TUserContext"></typeparam>
	/// <typeparam name="TInput"></typeparam>
	/// <typeparam name="TOutput"></typeparam>
	/// <returns></returns>
	public static List<LilikoiContainer> Scan<TUserContext, TInput, TOutput>(TUserContext context, Assembly assembly, Mount mount)
		where TUserContext : IMount
	{
		return Scan<TUserContext, TInput, TOutput>(context, assembly, () => mount);
	}

	/// <summary>
	///     Scan the provided assembly for all Lilikoi Containers matching the user context and return them
	///     Use a provided function to supply the Mount for each container.
	/// </summary>
	/// <param name="context">A user-supplied context potentially consumed by a target</param>
	/// <param name="assembly"></param>
	/// <param name="sourceMount"></param>
	/// <typeparam name="TUserContext"></typeparam>
	/// <typeparam name="TInput"></typeparam>
	/// <typeparam name="TOutput"></typeparam>
	/// <returns></returns>
	public static List<LilikoiContainer> Scan<TUserContext, TInput, TOutput>(TUserContext context, Assembly assembly, Func<Mount> sourceMount)
		where TUserContext : IMount
	{
		List<LilikoiContainer> containers = new();

		foreach (var type in assembly.GetTypes())
			containers.AddRange(Scan<TUserContext, TInput, TOutput>(context, type, sourceMount));

		return containers;
	}

	/// <summary>
	/// Scan the provided generic type for Lilikoi Containers.
	/// Use the last parameter to supply a mount for each container.
	/// </summary>
	/// <param name="context"></param>
	/// <param name="sourceMount"></param>
	/// <typeparam name="TUserContext"></typeparam>
	/// <typeparam name="TInput"></typeparam>
	/// <typeparam name="TOutput"></typeparam>
	/// <typeparam name="TType"></typeparam>
	/// <returns></returns>
	public static List<LilikoiContainer> Scan<TUserContext, TInput, TOutput, TType>(TUserContext context, Func<Mount> sourceMount)
		where TUserContext : IMount
		=> Scan<TUserContext, TInput, TOutput>(context, typeof(TType), sourceMount);

	public static List<LilikoiContainer> Scan<TUserContext, TInput, TOutput>(TUserContext context, Type type, Func<Mount> sourceMount)
		where TUserContext : IMount
	{
		List<LilikoiContainer> containers = new();

		foreach (var methodInfo in type.GetMethods())
			containers.AddRange(Scan<TUserContext, TInput, TOutput>(context, methodInfo, sourceMount));

		return containers;
	}

	/// <summary>
	/// Scan an individual method for lilikoi containers.
	/// Note that if there are multiple target attributes then there can be multiple containers returned.
	/// </summary>
	/// <param name="context"></param>
	/// <param name="methodInfo"></param>
	/// <param name="sourceMount"></param>
	/// <typeparam name="TUserContext"></typeparam>
	/// <typeparam name="TInput"></typeparam>
	/// <typeparam name="TOutput"></typeparam>
	/// <returns></returns>
	public static List<LilikoiContainer> Scan<TUserContext, TInput, TOutput>(TUserContext context, MethodInfo methodInfo, Func<Mount> sourceMount)
		where TUserContext : IMount
	{
		List<LilikoiContainer> containers = new();

		foreach (LkTargetBuilderAttribute attribute in methodInfo.GetCustomAttributes()
			         .Where(attribute => attribute
				         .GetType()
				         .IsSubclassOf(typeof(LkTargetBuilderAttribute))))
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

				containers.Add(compiler.Finish());
			}
		return containers;
	}
}
