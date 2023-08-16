//       ========================
//       Lilikoi.Benchmarks::SimpleInjectHost.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Compiler.Public;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Benchmarks.Mahogany.Applications.InjectSimple;

public class SimpleInjectHost
{
	[SimpleInjector] public Simple Injected;

	public object Execute()
	{
		return Injected.Execute();
	}

	public static Func<object, object> Build()
	{
		return LilikoiMethod.FromMethodInfo(typeof(SimpleInjectHost).GetMethod(nameof(Execute)))
			.Mount(new Mount())
			.Input<object>()
			.Output<object>()
			.Build()
			.Finish()
			.Compile<object, object>();
	}
}
