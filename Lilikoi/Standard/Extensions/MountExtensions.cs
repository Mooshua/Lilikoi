//       ========================
//       Lilikoi::MountExtensions.cs
//
// ->    Created: 01.02.2023
// ->    Bumped: 01.02.2023
//
// ->    Purpose:
//
//
//       ========================
using System.Runtime.CompilerServices;

using Lilikoi.Context;

namespace Lilikoi.Standard.Extensions;

public static class MountExtensions
{
	public static Mount RegisterFactory<TFactory, TUnderlying>(this Mount self, TFactory factory)
		where TFactory: IFactory<TUnderlying>
	{
		self.Store<IFactory<TUnderlying>>(factory);

		return self;
	}

	public static Mount RegisterSingleton<TSingleton>(this Mount self, TSingleton value)
		where TSingleton : class
	{
		self.Store(value);

		return self;
	}
}
