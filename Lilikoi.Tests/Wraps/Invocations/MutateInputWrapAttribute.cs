//       ========================
//       Lilikoi.Tests::MutateInputWrapAttribute.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 24.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Attributes;
using Lilikoi.Context;

#endregion

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