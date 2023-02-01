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
using Lilikoi.Attributes;
using Lilikoi.Context;

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
		var instance = Activator.CreateInstance(typeof(TInjectable), ConstructorParameters) as TInjectable;

		if (instance is null)
			throw new InvalidOperationException();

		return instance;
	}
}
