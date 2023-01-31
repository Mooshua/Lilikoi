//       ========================
//       Lilikoi.Tests::RespectsNonStandardReturns.cs
//       Distributed under the MIT License.
//
// ->    Created: 24.12.2022
// ->    Bumped: 24.12.2022
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Attributes;
using Lilikoi.Attributes.Builders;
using Lilikoi.Compiler.Public;
using Lilikoi.Compiler.Public.Utilities;
using Lilikoi.Context;
using Lilikoi.Tests.HelloWorld;

namespace Lilikoi.Tests.Wraps;

public class SelfInject
{
	public class NonStandardReturn : LkWrapBuilderAttribute
	{
		public override LkWrapAttribute Build(Mount mount)
		{
			return new TheReturn();
		}

		public override bool IsWrappable<TInput, TOutput>(Mount mount)
		{
			return true;
		}
	}

	public class TheReturn : LkWrapAttribute
	{
		[Console] public ConsoleInj Injectable;

		public override bool IsWrappable<TInput, TOutput>(Mount mount)
		{
			return true;
		}

		public override WrapResult<TOutput> Before<TInput, TOutput>(Mount mount, ref TInput input)
		{
			LilikoiInjector.Inject(mount, this);

			Assert.IsNotNull(Injectable);

			return WrapResult<TOutput>.Continue();
		}

		public override void After<TOutput>(Mount mount, ref TOutput output)
		{
			Assert.IsNotNull(Injectable);
			Assert.Pass();
		}
	}

	public class Host
	{

		[NonStandardReturn]
		public string Entry()
		{
			return "Entry";
		}
	}

	[Test]
	public void BasicSelfInject()
	{
		var method = typeof(Host).GetMethod("Entry");

		var build = LilikoiMethod.FromMethodInfo(method)
			.Input<object>()
			.Output<string>()
			.Mount(new Mount())
			.Build()
			.Finish();

		Console.WriteLine(build.ToString());

		var output = build.Run<Host, object, string>(new Host(), new object());

		Assert.Fail("Did not evaluate Assert.Pass() in WrapWithInjectionAttribute.");
	}
}
