//       ========================
//       Lilikoi.Tests::HelloWorldTest.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using System.Reflection;

using Lilikoi.Compiler.Public;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Tests.HelloWorld;

public class HelloWorldTest
{
	[Test]
	public async Task HelloWorld()
	{
		var attr = new ConsoleAttribute();

		Assert.IsNotNull(attr.Build(new Mount()));

		var build = LilikoiMethod.FromMethodInfo((MethodInfo)typeof(HelloWorldHost).GetMethod("HelloWorld"))
			.Input<object>()
			.Output<object>()
			.Mount(new Mount())
			.Build()
			.Finish();

		Console.WriteLine(build.ToString());

		build.Run<object, object>(new object());
	}
}
