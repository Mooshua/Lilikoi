//       ========================
//       Lilikoi::FactoryAttribute.cs
//
// ->    Created: 01.02.2023
// ->    Bumped: 01.02.2023
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Attributes;
using Lilikoi.Context;

namespace Lilikoi.Standard;

public class FactoryAttribute : LkInjectionAttribute
{
	public override bool IsInjectable<TInjectable>(Mount mount)
	{
		return mount.Has<IFactory<TInjectable>>();
	}

	public override TInjectable Inject<TInjectable>(Mount context)
	{
		return context.Get<IFactory<TInjectable>>().Create();
	}
}
