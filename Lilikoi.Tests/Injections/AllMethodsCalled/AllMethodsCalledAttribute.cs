//       ========================
//       Lilikoi.Tests::AllMethodsCalledAttribute.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Tests.Injections.AllMethodsCalled;

public class AllMethodsCalledAttribute : LkTypedInjectionAttribute<AllMethodsCalledInject>
{
	public override AllMethodsCalledInject Inject(Mount context)
	{
		AllMethodsCalledTest.Instance.InjectCalled = true;

		return new AllMethodsCalledInject();
	}

	public override void Deject(Mount context, AllMethodsCalledInject injected)
	{
		AllMethodsCalledTest.Instance.DejectCalled = true;
	}
}