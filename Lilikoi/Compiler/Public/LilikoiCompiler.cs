//       ========================
//       Lilikoi::LilikoiCompiler.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Attributes.Builders;
using Lilikoi.Compiler.Mahogany;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Compiler.Public;

public class LilikoiCompiler
{
	internal List<(LkParameterBuilderAttribute, Type)> ImplicitWildcards = new();

	internal List<LkWrapBuilderAttribute> ImplicitWraps = new();

	internal MahoganyCompiler Internal { get; set; }

	internal Mount Smuggler { get; } = new();

	public LilikoiMutator Mutator()
	{
		return new LilikoiMutator(Smuggler, this);
	}

	private void Mutators()
	{
		var mutators = MahoganyCompiler.MutatorsForMethod(Internal.Method.Entry);

		foreach (var lilikoiMutator in mutators)
			lilikoiMutator.Mutate(Mutator());
	}

	public LilikoiContainer Finish()
	{
		Mutators();

		Internal.HostFor();
		Internal.ParameterSafety();

		Internal.InjectionsFor(Internal.Method.Host);

		foreach (var implicitWrap in ImplicitWraps)
			Internal.ImplicitWrap(implicitWrap);
		Internal.WrapsFor();

		foreach (var (implicitWildcard, type) in ImplicitWildcards)
			Internal.ImplicitWildcard(implicitWildcard, type);

		Internal.ParametersFor();

		Internal.Apex();

		return new LilikoiContainer(Smuggler, Internal.Method.Lambda());
	}
}
