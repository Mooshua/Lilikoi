//       ========================
//       Lilikoi.Tests::MutateInputWrapAttribute.cs
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
using Lilikoi.Context;

namespace Lilikoi.Tests.Wraps.Invocations;

public class MutateInputWrapAttribute : LkWrapAttribute
{
	public override bool IsWrappable<TInput, TOutput>(Mount mount)
	{
		return true;
	}

	public override WrapResult<TOutput> Before<TInput, TOutput>(Mount mount, ref TInput input)
	{
		input = "Modified" as TInput;

		return WrapResult<TOutput>.Continue();
	}

	public override void After<TOutput>(Mount mount, ref TOutput output)
	{
		Assert.Fail("Pass block in Entry point not hit.");
	}
}
