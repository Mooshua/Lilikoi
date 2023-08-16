//       ========================
//       Lilikoi.Tests::AllMethodsCalledTest.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using System.Reflection;

using Lilikoi.Compiler.Public;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Tests.Injections.AllMethodsCalled;

public class AllMethodsCalledTest
{
	public static AllMethodsCalledCounter Instance;

	[Test]
	public void AllMethodsCalled()
	{
		var method = (MethodInfo)typeof(AllMethodsCalledHost).GetMethod("Entry")!;

		Instance = new AllMethodsCalledCounter();

		var build = LilikoiMethod.FromMethodInfo(method)
			.Input<AllMethodsCalledCounter>()
			.Output<object>()
			.Mount(new Mount())
			.Build()
			.Finish();

		Console.WriteLine(build.ToString());

		var value = build.Run<AllMethodsCalledCounter, string>(Instance);

		Assert.NotNull(value);

		Assert.IsTrue(Instance.InjectCalled, "Injection was not invoked");
		Assert.IsTrue(Instance.EntryCalled, "Entry was not invoked");
		Assert.IsTrue(Instance.DejectCalled, "Deject was not invoked");
		Assert.IsTrue(Instance.ParameterCalled, "Parameter was not invoked");
	}

	public class AllMethodsCalledCounter
	{
		public bool DejectCalled = false;
		public bool EntryCalled = false;

		public bool InjectCalled = false;
		public bool ParameterCalled = false;
	}
}
