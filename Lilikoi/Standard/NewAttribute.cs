//       ========================
//       Lilikoi::NewAttribute.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 23.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Attributes;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Standard;

public class NewAttribute : LkInjectionAttribute
{
	public NewAttribute(params object[] constructor)
	{
		ConstructorParameters = constructor;
	}

	protected object[] ConstructorParameters { get; set; }

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
