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

public class WrapTests
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

		[MutateInputWrap]
		public string ShouldModifyInput(string input)
		{
			Assert.AreEqual(input, "Modified");
			Assert.Pass("Modified entry");
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
	public void ModifiesInput()
	{
		var method = typeof(DummyHost).GetMethod("ShouldModifyInput");

		var build = LilikoiMethod.FromMethodInfo(method)
			.Input<string>()
			.Output<string>()
			.Mount(new Mount())
			.Build()
			.Finish();

		Console.WriteLine(build.ToString());

		var output = build.Run<DummyHost, string, string>(new DummyHost(), "Input");
	}

}
