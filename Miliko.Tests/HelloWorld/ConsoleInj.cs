//       ========================
//       Miliko.Tests::ConsoleInj.cs
//       Distributed under the MIT License.
//
// ->    Created: 05.12.2022
// ->    Bumped: 05.12.2022
//
// ->    Purpose:
//
//
//       ========================
namespace Miliko.Tests.Injects;

public class ConsoleInj
{
	public void Log(string value)
	{
		Console.WriteLine(value);
	}
}
