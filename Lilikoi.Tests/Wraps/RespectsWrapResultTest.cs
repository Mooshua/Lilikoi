//       ========================
//       Lilikoi.Tests::RespectsWrapResultTest.cs
//       Distributed under the MIT License.
//
// ->    Created: 24.12.2022
// ->    Bumped: 24.12.2022
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Core.Builder.Public;
using Lilikoi.Core.Context;
using Lilikoi.Tests.Injections.AllMethodsCalled;
using Lilikoi.Tests.Wraps.Invocations;

namespace Lilikoi.Tests.Wraps;

public class RespectsWrapResultTest
{
	public class DummyHost
	{
		[HaltWrap]
		public string ShouldHalt()
		{
			Assert.Fail("Entry point invoked");

			return "Entry";
		}

		[ContinueWrap]
		public string ShouldContinue()
		{

			return "Entry";
		}

		[ModifyWrap]
		public string ShouldModify()
		{

			return "Entry";
		}

		[WrapWithInjection]
		public string HasInjection()
		{
			return "Entry";
		}
	}

	[Test]
	public void Halts()
	{
		var method = typeof(DummyHost).GetMethod("ShouldContinue");

		var build = LilikoiMethod.FromMethodInfo(method)
			.Input<object>()
			.Output<string>()
			.Mount(new Mount())
			.Build()
			.Finish();

		Console.WriteLine(build.ToString());

		var output = build.Run<DummyHost, object, string>(new DummyHost(), new object());

		Assert.AreEqual(output, "Entry");
	}

	[Test]
	public void Continues()
	{
		var method = typeof(DummyHost).GetMethod("ShouldHalt");

		var build = LilikoiMethod.FromMethodInfo(method)
			.Input<object>()
			.Output<string>()
			.Mount(new Mount())
			.Build()
			.Finish();

		Console.WriteLine(build.ToString());

		var output = build.Run<DummyHost, object, string>(new DummyHost(), new object());

		Assert.AreEqual(output, "Before");
	}

	[Test]
	public void Modifies()
	{
		var method = typeof(DummyHost).GetMethod("ShouldModify");

		var build = LilikoiMethod.FromMethodInfo(method)
			.Input<object>()
			.Output<string>()
			.Mount(new Mount())
			.Build()
			.Finish();

		Console.WriteLine(build.ToString());

		var output = build.Run<DummyHost, object, string>(new DummyHost(), new object());

		Assert.AreEqual(output, "After");
	}

	[Test]
	public void Injects()
	{
		var method = typeof(DummyHost).GetMethod("HasInjection");

		var build = LilikoiMethod.FromMethodInfo(method)
			.Input<object>()
			.Output<string>()
			.Mount(new Mount())
			.Build()
			.Finish();

		Console.WriteLine(build.ToString());

		var output = build.Run<DummyHost, object, string>(new DummyHost(), new object());

		Assert.Fail("Did not evaluate Assert.Pass() in WrapWithInjectionAttribute.");
	}
}
