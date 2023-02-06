//       ========================
//       Lilikoi.Tests::WildcardTest.cs
//
// ->    Created: 02.02.2023
// ->    Bumped: 02.02.2023
//
// ->    Purpose:
//
//
//       ========================
using Lilikoi.Attributes;
using Lilikoi.Attributes.Static;
using Lilikoi.Attributes.Typed;
using Lilikoi.Compiler.Public;
using Lilikoi.Context;

namespace Lilikoi.Tests.Mutator.Wildcards;

public class WildcardTest
{
	public const string WILDCARD_VALUE = "Uno!";

	[Test]
	public void CanInjectWildcard()
	{
		var mount = new Mount();

		var container = LilikoiMethod.FromMethodInfo(typeof(WildcardHost).GetMethod("Entry"))
			.Input<object>()
			.Output<string>()
			.Mount(mount)
			.Build()
			.Finish();

		var host = new WildcardHost();
		var value = container.Run<WildcardHost, object, string>(host, host);

		Assert.AreEqual(WILDCARD_VALUE, value);
	}

	public class WildcardAttribute : LkMutatorAttribute
	{
		public override void Mutate(LilikoiMutator mutator)
		{
			mutator.Wildcard<string>(new WildcardInjector());
		}
	}

	public class WildcardInjector : LkTypedParameterAttribute<string, object>
	{
		public override string Inject(Mount context, object input)
		{
			return WILDCARD_VALUE;
		}
	}

	public class WildcardHost
	{

		[Wildcard]
		public string Entry(string thing)
		{
			return thing;
		}

	}
}
