//       ========================
//       Lilikoi.Tests::HelloWorldHost.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

#endregion

namespace Lilikoi.Tests.HelloWorld;

public class HelloWorldHost
{
	[Console] public ConsoleInj ConsoleImpl;

	public object HelloWorld()
	{
		ConsoleImpl.Log("Hello!");

		return ConsoleImpl;
	}
}