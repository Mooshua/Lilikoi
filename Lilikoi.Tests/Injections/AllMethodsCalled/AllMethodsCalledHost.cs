//       ========================
//       Lilikoi.Tests::AllMethodsCalledHost.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
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