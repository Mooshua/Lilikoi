//       ========================
//       Miliko.Tests::AllMethodsCalledTest.cs
//       Distributed under the MIT License.
//
// ->    Created: 19.12.2022
// ->    Bumped: 19.12.2022
//
// ->    Purpose:
//
//
//       ========================
using System.Reflection;

using Miliko.Attributes.Builder.Public;
using Miliko.Tests.Hosts;
using Miliko.Tests.Injects.Attributes;

namespace Miliko.Tests.Injections.AllMethodsCalled;

public class AllMethodsCalledTest
{
	public static AllMethodsCalledTest Instance;

	public bool InjectCalled = false;
	public bool DejectCalled = false;
	public bool EntryCalled = false;

	[Test]
	public void AllMethodsCalled()
	{

		var method = (MethodInfo)typeof(AllMethodsCalledHost).GetMethod("Entry")!;

		Instance = this;

		var build = MilikoMethod.FromMethodInfo(method)
			.Input<object>()
			.Output<object>()
			.Build()
			.Finish();

		Console.WriteLine(build.ToString());

		build.Run<AllMethodsCalledHost, object, object>(new AllMethodsCalledHost() { Test = this }, new object());


		Assert.IsTrue(InjectCalled, "Injection was not invoked");
		Assert.IsTrue(EntryCalled, "Entry was not invoked");
		Assert.IsTrue(DejectCalled, "Deject was not invoked");
	}
}
