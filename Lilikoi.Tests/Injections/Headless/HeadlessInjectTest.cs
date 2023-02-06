//       ========================
//       Lilikoi.Tests::HeadlessInjectTest.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 23.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Compiler.Public.Utilities;
using Lilikoi.Tests.Injections.AllMethodsCalled;

#endregion

namespace Lilikoi.Tests.Injections.Headless;

public class HeadlessInjectTest
{
	[Test]
	public void HeadlessAllMethodsCalled()
	{
		AllMethodsCalledTest.Instance = new AllMethodsCalledTest.AllMethodsCalledCounter();

		var counter = AllMethodsCalledTest.Instance;

		var injector = LilikoiInjector.CreateInjector<AllMethodsCalledHost>();

		var host = new AllMethodsCalledHost();

		Assert.IsNull(host.Inject);
		Assert.IsFalse(counter.InjectCalled);

		injector(host);

		Assert.IsTrue(counter.InjectCalled);
		Assert.IsNotNull(host.Inject);
	}
}