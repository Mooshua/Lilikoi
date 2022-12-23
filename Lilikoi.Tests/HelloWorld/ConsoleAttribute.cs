//       ========================
//       Lilikoi.Tests::ConsoleAttribute.cs
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

using Lilikoi.Core.Attributes.Typed;

#endregion

namespace Lilikoi.Tests.HelloWorld;

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