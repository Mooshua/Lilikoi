//       ========================
//       Miliko.Core::MilikoCompiler.cs
//       Distributed under the MIT License.
//
// ->    Created: 06.12.2022
// ->    Bumped: 06.12.2022
//
// ->    Purpose:
//
//
//       ========================
using System.Linq;
using System.Reflection;

using Miliko.API.Attributes.Builders;
using Miliko.API.Builder.Mahogany;

namespace Miliko.Attributes.Builder.Public;

public class MilikoCompiler
{
	internal MahoganyCompiler Internal { get; set; }


	public MilikoCompiler Emit<TWrap>()
		where TWrap: MkWrapBuilderAttribute
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
