//       ========================
//       Lilikoi.Tests::WrapTests.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 24.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Compiler.Public;
using Lilikoi.Context;
using Lilikoi.Tests.Wraps.Invocations;

#endregion

namespace Lilikoi.Tests.Wraps;

public class WrapTests
{
	[Test]
	public void Halts()
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

		Assert.AreEqual("Before", output);
	}

	[Test]
	public void Continues()
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

		Assert.Fail("Reached exit point without passing");
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

		Assert.AreEqual("After", output);
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

		Assert.Fail("Reached exit point without passing");
	}

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
}