﻿//       ========================
//       Lilikoi::MountAttribute.cs
//       Distributed under the MIT License.
//
// ->    Created: 23.12.2022
// ->    Bumped: 23.12.2022
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Attributes;
using Lilikoi.Context;

namespace Lilikoi.Standard;

public class SingletonAttribute : LkInjectionAttribute
{
	public override bool IsInjectable<TInjectable>(Mount mount)
	{
		return mount.Has<TInjectable>();
	}

	public override TInjectable Inject<TInjectable>(Mount context)
	{
		return context.Get<TInjectable>()!;
	}
}