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
using Lilikoi.Core.Context;

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
