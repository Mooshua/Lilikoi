//       ========================
//       Lilikoi.Tests::HeadlessInjectTest.cs
//       Distributed under the MIT License.
//
// ->    Created: 23.12.2022
// ->    Bumped: 23.12.2022
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Core.Builder.Public.Utilities;
using Lilikoi.Tests.Injections.AllMethodsCalled;

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
