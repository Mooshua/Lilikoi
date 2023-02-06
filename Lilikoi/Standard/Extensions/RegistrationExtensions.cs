//       ========================
//       Lilikoi::RegistrationExtensions.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 01.02.2023
// ->    Bumped: 06.02.2023
//       ========================
namespace Lilikoi.Standard.Extensions;

public static class RegistrationExtensions
{
	public static MountBuilder RegisterFactory<TFactory, TUnderlying>(this MountBuilder self, TFactory factory)
		where TFactory : IFactory<TUnderlying>
	{
		self.Store<IFactory<TUnderlying>>(factory);

		return self;
	}

	public static MountBuilder RegisterSingleton<TSingleton>(this MountBuilder self, TSingleton value)
		where TSingleton : class
	{
		self.Store(value);

		return self;
	}
}
