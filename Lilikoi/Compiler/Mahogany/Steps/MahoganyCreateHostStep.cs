//       ========================
//       Lilikoi::MahoganyCreateHostStep.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 10.08.2023
// ->    Bumped: 10.08.2023
//       ========================
using System.Linq.Expressions;
using System.Reflection;

using Lilikoi.Attributes.Builders;

namespace Lilikoi.Compiler.Mahogany.Steps;

public class MahoganyCreateHostStep
{
	public MahoganyCreateHostStep(MahoganyMethod method)
	{
		Method = method;
	}

	public MahoganyMethod Method { get; set; }

	public Expression Generate()
	{
		var host = Method.Named(MahoganyConstants.HOST_VAR);

		var creation = Expression.New(Method.Host);
		var assignment = Expression.Assign(host, creation);

		return assignment;
	}
}
