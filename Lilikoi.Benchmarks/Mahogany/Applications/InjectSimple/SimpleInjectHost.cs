//       ========================
//       Lilikoi.Benchmarks::InjectHellHost.cs
//       Distributed under the MIT License.
//
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Compiler.Public;

namespace Lilikoi.Benchmarks.Mahogany.Applications.InjectSimple;

public class SimpleInjectHost
{

	[SimpleInjector] public Simple Injected;

	public bool Execute()
	{
		return Injected.Execute();
	}

	public static Func<SimpleInjectHost, bool, bool> Build()
	{
		return LilikoiMethod.FromMethodInfo(typeof(SimpleInjectHost).GetMethod(nameof(Execute)))
			.Input<bool>()
			.Output<bool>()
			.Build()
			.Finish()
			.Compile<SimpleInjectHost, bool, bool>();
	}

}
