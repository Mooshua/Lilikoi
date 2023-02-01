//       ========================
//       Lilikoi.Tests::AllMethodsCalledHost.cs
//       Distributed under the MIT License.
//
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
//
// ->    Purpose:
//
//
//       ========================
namespace Lilikoi.Tests.Injections.AllMethodsCalled;

public class AllMethodsCalledHost
{

	[AllMethodsCalled] public AllMethodsCalledInject Inject;

	public object Entry(AllMethodsCalledTest.AllMethodsCalledCounter test, [AllMethodsCalledParameter] object param)
	{
		test.EntryCalled = true;

		Assert.IsNotNull(param);

		Assert.IsNotNull(Inject);
		Assert.IsTrue(Inject.IsNotNull());

		return new object();
	}
}
