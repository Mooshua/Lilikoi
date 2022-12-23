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

using Lilikoi.Core.Attributes.Builders;
using Lilikoi.Core.Builder.Mahogany;

#endregion

namespace Lilikoi.Core.Builder.Public;

public class LilikoiCompiler
{
	internal MahoganyCompiler Internal { get; set; }


	public LilikoiCompiler Emit<TWrap>()
		where TWrap : LkWrapBuilderAttribute
	{
		return this;
	}

	public LilikoiContainer Finish()
	{
		Internal.ParameterSafety();
		Internal.InjectionsFor(Internal.Method.Host);
		Internal.Apex();

		return new LilikoiContainer()
		{
			Body = Internal.Method.Lambda()
		};
	}
}
