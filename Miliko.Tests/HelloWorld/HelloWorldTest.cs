//       ========================
//       Miliko.Tests::HelloWorldTest.cs
//       Distributed under the MIT License.
//
// ->    Created: 05.12.2022
// ->    Bumped: 05.12.2022
//
// ->    Purpose:
//
//
//       ========================
using System.Linq.Expressions;
using System.Reflection;

using Miliko.API.Attributes;
using Miliko.API.Builder;
using Miliko.Attributes.Builder.Public;
using Miliko.Tests.Hosts;
using Miliko.Tests.Injects;
using Miliko.Tests.Injects.Attributes;

namespace Miliko.Tests.HelloWorld;

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
