//       ========================
//       Lilikoi.Tests::HaltWrapAttribute.cs
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

public class HaltWrapAttribute : LkWrapAttribute
{
	public override bool IsWrappable<TInput, TOutput>(Mount mount)
	{
		return true;
	}

	public override WrapResult<TOutput> Before<TInput, TOutput>(Mount mount, ref TInput input)
	{
		return WrapResult<TOutput>.Stop("Before" as TOutput);
	}

	public override void After<TOutput>(Mount mount, ref TOutput output)
	{
		Assert.Fail("After method invoked after Stop call");
	}
}