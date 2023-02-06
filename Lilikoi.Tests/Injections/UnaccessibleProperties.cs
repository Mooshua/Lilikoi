//       ========================
//       Lilikoi.Tests::UnaccessibleProperties.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 24.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using System.Reflection;

using Lilikoi.Compiler.Public;
using Lilikoi.Context;
using Lilikoi.Tests.HelloWorld;

#endregion

namespace Lilikoi.Tests.Injections;

public class UnaccessibleProperties
{
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

	public class Host
	{
		[Console] private ConsoleInj Private;

		[Console] protected ConsoleInj Protected;

		public string Entry()
		{
			Assert.IsNotNull(Private);
			Assert.IsNotNull(Protected);

			//Assert.IsNotNull(PrivateAndRead);
			//Assert.IsNotNull(PublicAndRead);
			//Assert.IsNotNull(PublicAndPrivateWrite);

			Assert.Pass("All values present");

			return "Entry";
		}
	}
}