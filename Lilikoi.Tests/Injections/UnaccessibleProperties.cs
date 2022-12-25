//       ========================
//       Lilikoi.Tests::UnaccessibleProperties.cs
//       Distributed under the MIT License.
//
// ->    Created: 24.12.2022
// ->    Bumped: 24.12.2022
//
// ->    Purpose:
//
//
//       ========================
using System.Reflection;

using Lilikoi.Core.Builder.Public;
using Lilikoi.Core.Context;
using Lilikoi.Tests.HelloWorld;
using Lilikoi.Tests.Injections.AllMethodsCalled;

namespace Lilikoi.Tests.Injections;

public class UnaccessibleProperties
{
	public class Host
	{
		[Console] private ConsoleInj Private;



		public string Entry()
		{
			Assert.IsNotNull(Private);
			//Assert.IsNotNull(PrivateAndRead);
			//Assert.IsNotNull(PublicAndRead);
			//Assert.IsNotNull(PublicAndPrivateWrite);

			Assert.Pass("All values present");

			return "Entry";
		}
	}

	[Test]
	public void UnaccessablePropertiesStillInject()
	{
		var method = (MethodInfo)typeof(Host).GetMethod("Entry")!;

		var build = LilikoiMethod.FromMethodInfo(method)
			.Input<string>()
			.Output<string>()
			.Mount(new Mount())
			.Build()
			.Finish();

		Console.WriteLine(build.ToString());

		build.Run<Host, string, string>(new Host(), "Input");

		Assert.Fail("Entry point not executed");
	}
}
