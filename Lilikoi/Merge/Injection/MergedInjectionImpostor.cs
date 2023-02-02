//       ========================
//       Lilikoi::MergedInjectionActor.cs
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

namespace Lilikoi.Merge.Injection;

public class MergedInjectionImpostor : LkInjectionAttribute
{
	protected InjectionMerger Merged { get; }

	public MergedInjectionImpostor(InjectionMerger merged)
	{
		Merged = merged;
	}

	public override bool IsInjectable<TInjectable>(Mount mount)
	{
		return Merged.Has<TInjectable>(mount);
	}

	public override TInjectable Inject<TInjectable>(Mount context)
	{
		return Merged.Inject<TInjectable>(context);
	}

	public override void Deject<TInjectable>(Mount context, TInjectable injected)
	{
		Merged.Deject(context, injected);
	}
}
