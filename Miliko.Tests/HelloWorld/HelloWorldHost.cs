//       ========================
//       Miliko.Tests::HelloWorldHost.cs
//       Distributed under the MIT License.
//
// ->    Created: 05.12.2022
// ->    Bumped: 05.12.2022
//
// ->    Purpose:
//
//
//       ========================
using Miliko.Tests.Injects;
using Miliko.Tests.Injects.Attributes;

namespace Miliko.Tests.Hosts;

public class HelloWorldHost
{
	[Console] public ConsoleInj ConsoleImpl { get; set; }

	public object HelloWorld()
	{
		ConsoleImpl.Log("Hello!");

		return ConsoleImpl;
	}
}
