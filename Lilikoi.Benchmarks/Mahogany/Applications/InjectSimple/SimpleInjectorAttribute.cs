//       ========================
//       Lilikoi.Benchmarks::SimpleInjectorAttribute.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using Lilikoi.Attributes.Typed;
using Lilikoi.Context;

#endregion

namespace Lilikoi.Benchmarks.Mahogany.Applications.InjectSimple;

public class SimpleInjectorAttribute : LkTypedInjectionAttribute<Simple>
{
	public override Simple Inject(Mount mount)
	{
		return Simple.Create();
	}
}