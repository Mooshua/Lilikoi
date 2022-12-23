//       ========================
//       Lilikoi.Tests::HelloWorldTest.cs
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

using System.Reflection;

using Lilikoi.Core.Builder.Public;

#endregion

namespace Lilikoi.Tests.HelloWorld;

public class HelloWorldTest
{
	[Test]
	public void HelloWorld()
	{
		var attr = new ConsoleAttribute();

		Assert.IsNotNull(attr.Build());

		var build = MilikoMethod.FromMethodInfo((MethodInfo)typeof(HelloWorldHost).GetMethod("HelloWorld"))
			.Input<object>()
			.Output<object>()
			.Build()
			.Finish();

		Console.WriteLine(build.ToString());

		build.Run<HelloWorldHost, object, object>(new HelloWorldHost(), new object());
	}
}