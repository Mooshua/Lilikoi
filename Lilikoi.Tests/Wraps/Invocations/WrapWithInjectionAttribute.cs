//       ========================
//       Lilikoi.Tests::WrapWithInjectionAttribute.cs
//       Distributed under the MIT License.
//
// ->    Created: 24.12.2022
// ->    Bumped: 24.12.2022
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Core.Attributes;
using Lilikoi.Core.Context;
using Lilikoi.Tests.HelloWorld;

namespace Lilikoi.Tests.Wraps.Invocations;

public class WrapWithInjectionAttribute : LkWrapAttribute
{
	[Console]
	public ConsoleInj Injected { get; set; }

	public override bool IsWrappable<TInput, TOutput>(Mount mount)
	{
		return true;
	}

	public override WrapResult<TOutput> Before<TInput, TOutput>(Mount mount, ref TInput input)
	{
		Assert.IsNotNull(Injected);
		Injected.Log("Before");

		return WrapResult<TOutput>.Continue();
	}

	public override void After<TOutput>(Mount mount, ref TOutput output)
	{
		Assert.IsNotNull(Injected);
		Injected.Log("After");

		Assert.Pass("Reached after with valid state");
	}
}
