//       ========================
//       Lilikoi::LkInjectionImpostor.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 01.02.2023
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Attributes.Builders;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Attributes.Impostor;

public class LkInjectionImpostor : LkInjectionBuilderAttribute
{
	public LkInjectionAttribute Impostor { get; }

	public sealed override LkInjectionAttribute Build(Mount mount)
	{
		return Impostor;
	}

	public sealed override bool IsInjectable<TInjectable>(Mount mount)
	{
		return Impostor.IsInjectable<TInjectable>(mount);
	}
}