//       ========================
//       Miliko.Tests::AllMethodsCalledAttribute.cs
//       Distributed under the MIT License.
//
// ->    Created: 19.12.2022
// ->    Bumped: 19.12.2022
//
// ->    Purpose:
//
//
//       ========================
using Miliko.API.Attributes;

namespace Miliko.Tests.Injections.AllMethodsCalled;

public class AllMethodsCalledAttribute : MkTypedInjectionAttribute<AllMethodsCalledInject>
{
	public AllMethodsCalledTest Test = AllMethodsCalledTest.Instance;

	public override AllMethodsCalledInject Inject()
	{
		Test.InjectCalled = true;

		return new AllMethodsCalledInject();
	}

	public override void Deject(AllMethodsCalledInject injected)
	{
		Test.DejectCalled = true;

	}
}
