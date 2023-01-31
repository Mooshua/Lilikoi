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
	protected object[] ConstructorParameters { get; set; }

	public NewAttribute(params object[] constructor)
	{
		ConstructorParameters = constructor;
	}

	public override bool IsInjectable<TInjectable>(Mount mount)
	{
		return Activator.CreateInstance(typeof(TInjectable), ConstructorParameters) as TInjectable is not null;
	}

	public override TInjectable Inject<TInjectable>(Mount context)
	{
		return Activator.CreateInstance(typeof(TInjectable), ConstructorParameters) as TInjectable;
	}
}
