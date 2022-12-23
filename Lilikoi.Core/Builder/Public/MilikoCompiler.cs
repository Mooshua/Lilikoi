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

public class MilikoCompiler
{
	internal MahoganyCompiler Internal { get; set; }


	public MilikoCompiler Emit<TWrap>()
		where TWrap : MkWrapBuilderAttribute
	{
		return this;
	}

	public MilikoContainer Finish()
	{
		Internal.ParameterSafety();
		Internal.InjectionsFor(Internal.Method.Host);
		Internal.Apex();

		return new MilikoContainer()
		{
			Body = Internal.Method.Lambda()
		};
	}
}