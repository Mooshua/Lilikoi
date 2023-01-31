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

using System.Collections.Generic;

using Lilikoi.Core.Attributes.Builders;
using Lilikoi.Core.Compiler.Mahogany;

#endregion

namespace Lilikoi.Core.Compiler.Public;

public class LilikoiCompiler
{
	internal MahoganyCompiler Internal { get; set; }

	internal List<LkWrapBuilderAttribute> ImplicitWraps = new List<LkWrapBuilderAttribute>();
	internal List<LkParameterBuilderAttribute> ImplicitWildcards = new List<LkParameterBuilderAttribute>();

	public LilikoiMutator Mutator()
	{
		return new LilikoiMutator()
		{
			Compiler = this
		};
	}

	public LilikoiContainer Finish()
	{
		Internal.ParameterSafety();
		Internal.InjectionsFor(Internal.Method.Host);

		foreach (var implicitWrap in ImplicitWraps)
		{
			Internal.ImplicitWrap(implicitWrap);
		}
		Internal.WrapsFor();
		Internal.ParametersFor();

		Internal.Apex();

		return new LilikoiContainer()
		{
			Body = Internal.Method.Lambda()
		};
	}
}
