//       ========================
//       Lilikoi::NewAttribute.cs
//       Distributed under the MIT License.
//
// ->    Created: 23.12.2022
// ->    Bumped: 23.12.2022
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Core.Attributes;
using Lilikoi.Core.Context;

namespace Lilikoi.Standard;

public class NewAttribute : LkInjectionAttribute
{
	public override bool IsInjectable<TInjectable>(Mount mount)
	{
		return true;
	}

	public override TInjectable Inject<TInjectable>(Mount context)
	{
		return Activator.CreateInstance<TInjectable>();
	}
}
