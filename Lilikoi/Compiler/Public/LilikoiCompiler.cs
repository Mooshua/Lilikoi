//       ========================
//       Lilikoi.Core::MilikoCompiler.cs
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

using Lilikoi.Attributes.Builders;
using Lilikoi.Attributes.Static;
using Lilikoi.Compiler.Mahogany;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Compiler.Public;

public class LilikoiCompiler
{
	internal MahoganyCompiler Internal { get; set; }

	internal Mount Smuggler { get; } = new Mount();

	internal List<LkWrapBuilderAttribute> ImplicitWraps = new List<LkWrapBuilderAttribute>();
	internal List<(LkParameterBuilderAttribute, Type)> ImplicitWildcards = new List<(LkParameterBuilderAttribute, Type)>();

	public LilikoiMutator Mutator()
	{
		return new LilikoiMutator(Smuggler, this);
	}

	private void Mutators()
	{
		var mutators = MahoganyCompiler.MutatorsForMethod(Internal.Method.Entry);

		foreach (LkMutatorAttribute lilikoiMutator in mutators)
		{
			lilikoiMutator.Mutate(Mutator());
		}
	}

	public LilikoiContainer Finish()
	{
		Mutators();

		Internal.ParameterSafety();
		Internal.InjectionsFor(Internal.Method.Host);

		foreach (var implicitWrap in ImplicitWraps)
		{
			Internal.ImplicitWrap(implicitWrap);
		}
		Internal.WrapsFor();

		foreach (var (implicitWildcard, type) in ImplicitWildcards)
		{
			Internal.ImplicitWildcard(implicitWildcard, type);
		}

		Internal.ParametersFor();

		Internal.Apex();

		return new LilikoiContainer(Smuggler, Internal.Method.Lambda());
	}
}
