//       ========================
//       Lilikoi.Tests::SmuggleHost.cs
//
// ->    Created: 02.02.2023
// ->    Bumped: 02.02.2023
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Attributes.Static;
using Lilikoi.Compiler.Public;
using Lilikoi.Context;

namespace Lilikoi.Tests.Mutator.Smuggling;

public class SmuggleTest
{
	public const string SMUGGLED_VALUE = "Smuggled";

	[Test]
	public void CanSmuggle()
	{
		var mount = new Mount();

		var container = LilikoiMethod.FromMethodInfo(typeof(SmuggleHost).GetMethod("Entry"))
			.Input<object>()
			.Output<object>()
			.Mount(mount)
			.Build()
			.Finish();

		Assert.AreEqual(SMUGGLED_VALUE, container.Get<string>());
		Assert.AreNotEqual(SMUGGLED_VALUE, mount.Get<string>());
	}

	public class SmuggleAttribute : LkMutatorAttribute
	{
		public override void Mutate(LilikoiMutator mutator)
		{
			mutator.Store(SMUGGLED_VALUE);
		}
	}

	public class SmuggleHost
	{

		[Smuggle]
		public object Entry()
		{
			return this;
		}

	}
}
