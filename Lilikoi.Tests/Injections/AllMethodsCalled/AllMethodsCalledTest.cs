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
	public static AllMethodsCalledTest Instance;
	public bool DejectCalled = false;
	public bool EntryCalled = false;

	public bool InjectCalled = false;
	public bool ParameterCalled = false;

	[Test]
	public void AllMethodsCalled()
	{
		var method = (MethodInfo)typeof(AllMethodsCalledHost).GetMethod("Entry")!;

		Instance = this;

		var build = LilikoiMethod.FromMethodInfo(method)
			.Input<AllMethodsCalledTest>()
			.Output<object>()
			.Mount(new Mount())
			.Build()
			.Finish();

		Console.WriteLine(build.ToString());

		build.Run<AllMethodsCalledHost, AllMethodsCalledTest, object>(new AllMethodsCalledHost(), this);


		Assert.IsTrue(InjectCalled, "Injection was not invoked");
		Assert.IsTrue(EntryCalled, "Entry was not invoked");
		Assert.IsTrue(DejectCalled, "Deject was not invoked");
		Assert.IsTrue(ParameterCalled, "Parameter was not invoked");
	}
}
