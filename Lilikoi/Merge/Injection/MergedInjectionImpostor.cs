//       ========================
//       Lilikoi::MergedInjectionImpostor.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 01.02.2023
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Attributes;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Merge.Injection;

public class MergedInjectionImpostor : LkInjectionAttribute
{
	public MergedInjectionImpostor(InjectionMerger merged)
	{
		Merged = merged;
	}

	protected InjectionMerger Merged { get; }

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