//       ========================
//       Lilikoi.Benchmarks::SimpleInjectHost.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Compiler.Public;

#endregion

namespace Lilikoi.Benchmarks.Mahogany.Applications.InjectSimple;

public class SimpleInjectHost
{
	[SimpleInjector] public Simple Injected;

	public bool Execute()
	{
		return Injected.Execute();
	}

	public static Func<bool, bool> Build()
	{
		return LilikoiMethod.FromMethodInfo(typeof(SimpleInjectHost).GetMethod(nameof(Execute)))
			.Input<bool>()
			.Output<bool>()
			.Build()
			.Finish()
			.Compile<bool, bool>();
	}
}
