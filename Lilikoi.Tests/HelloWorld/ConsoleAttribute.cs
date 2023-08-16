//       ========================
//       Lilikoi.Tests::ConsoleAttribute.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Tests.HelloWorld;

public class ConsoleAttribute : LkTypedInjectionAttribute<ConsoleInj>
{
	public override ConsoleInj Inject(Mount context)
	{
		Console.WriteLine("Creating console inj!");
		return new ConsoleInj();
	}

	public override void Deject(Mount context, ConsoleInj injected)
	{
		Console.WriteLine("Dejecting console inj!");
		return;
	}
}