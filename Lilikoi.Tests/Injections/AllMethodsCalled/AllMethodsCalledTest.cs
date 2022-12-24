//       ========================
//       Lilikoi.Tests::AllMethodsCalledTest.cs
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

using System.Reflection;

using Lilikoi.Core.Builder.Public;
using Lilikoi.Core.Context;

#endregion

namespace Lilikoi.Tests.Injections.AllMethodsCalled;

public class AllMethodsCalledTest
{
	public static AllMethodsCalledCounter Instance;

	public class AllMethodsCalledCounter
	{
		public bool DejectCalled = false;
		public bool EntryCalled = false;

		public bool InjectCalled = false;
		public bool ParameterCalled = false;
	}

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

		build.Run<AllMethodsCalledHost, AllMethodsCalledCounter, object>(new AllMethodsCalledHost(), Instance);


		Assert.IsTrue(Instance.InjectCalled, "Injection was not invoked");
		Assert.IsTrue(Instance.EntryCalled, "Entry was not invoked");
		Assert.IsTrue(Instance.DejectCalled, "Deject was not invoked");
		Assert.IsTrue(Instance.ParameterCalled, "Parameter was not invoked");
	}
}
