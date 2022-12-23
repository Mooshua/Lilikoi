//       ========================
//       Lilikoi.Tests::HelloWorldHost.cs
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

#endregion

namespace Lilikoi.Tests.HelloWorld;

public class HelloWorldHost
{
	[Console]
	public ConsoleInj ConsoleImpl { get; set; }

	public object HelloWorld()
	{
		ConsoleImpl.Log("Hello!");

		return ConsoleImpl;
	}
}