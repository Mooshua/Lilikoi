//       ========================
//       Lilikoi::LkInjectionImpostor.cs
//
// ->    Created: 01.02.2023
// ->    Bumped: 01.02.2023
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Attributes.Builders;
using Lilikoi.Context;

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
