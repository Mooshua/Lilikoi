//       ========================
//       Lilikoi::MountBuilder.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 06.02.2023
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Context;

#endregion

namespace Lilikoi.Standard.Extensions;

public class MountBuilder : Mount
{
	public MountBuilder() : base()
	{
	}

	public MountBuilder(Mount self) : base(self)
	{
	}
}
