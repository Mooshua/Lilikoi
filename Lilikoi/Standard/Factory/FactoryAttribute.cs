//       ========================
//       Lilikoi::FactoryAttribute.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 01.02.2023
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Attributes;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Standard.Factory;

public class FactoryAttribute : LkInjectionAttribute
{
	public override bool IsInjectable<TInjectable>(Mount mount)
	{
		return mount.Has<IFactory<TInjectable>>();
	}

	public override TInjectable Inject<TInjectable>(Mount context)
	{
		var factory = context.Get<IFactory<TInjectable>>();

		if (factory is null)
			throw new InvalidOperationException($"No factory for type {typeof(TInjectable).FullName} exists.");

		return factory.Create();
	}
}
