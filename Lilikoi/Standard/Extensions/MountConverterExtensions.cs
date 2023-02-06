//       ========================
//       Lilikoi::MountConverterExtensions.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 06.02.2023
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Context;

#endregion

namespace Lilikoi.Standard.Extensions;

public static class MountConverterExtensions
{
	public static MountBuilder AsBuilder(this Mount self)
	{
		return new MountBuilder(self);
	}
}
