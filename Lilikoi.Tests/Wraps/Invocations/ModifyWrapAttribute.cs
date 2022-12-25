//       ========================
//       Lilikoi.Tests::ModifyWrapAttribute.cs
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

namespace Lilikoi.Tests.Wraps.Invocations;

public class ModifyWrapAttribute: LkWrapAttribute
{
	public override bool IsWrappable<TInput, TOutput>(Mount mount)
	{
		return true;
	}

	public override WrapResult<TOutput> Before<TInput, TOutput>(Mount mount, ref TInput input)
	{
		return WrapResult<TOutput>.Continue();
	}

	public override void After<TOutput>(Mount mount, ref TOutput output)
	{
		output = "After" as TOutput;
	}
}
