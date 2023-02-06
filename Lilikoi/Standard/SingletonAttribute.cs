//       ========================
//       Lilikoi::SingletonAttribute.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 23.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Attributes;
using Lilikoi.Context;

#endregion

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
