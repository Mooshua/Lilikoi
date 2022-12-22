//       ========================
//       Miliko.Tests::ConsoleAttribute.cs
//       Distributed under the MIT License.
//
// ->    Created: 05.12.2022
// ->    Bumped: 05.12.2022
//
// ->    Purpose:
//
//
//       ========================
using Miliko.API.Attributes;

namespace Miliko.Tests.Injects.Attributes;

public class ConsoleAttribute : MkTypedInjectionAttribute<ConsoleInj>
{
	public override ConsoleInj Inject()
	{
		Console.WriteLine("Creating console inj!");
		return new ConsoleInj();
	}

	public override void Deject(ConsoleInj injected)
	{
		Console.WriteLine("Dejecting console inj!");
		return;
	}
}
