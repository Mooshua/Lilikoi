//       ========================
//       Lilikoi.Tests::AllMethodsCalledAttribute.cs
//       Distributed under the MIT License.
//
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
//
// ->    Purpose:
//
//
//       ========================
#region

using Lilikoi.Core.Attributes.Typed;
using Lilikoi.Core.Context;

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
