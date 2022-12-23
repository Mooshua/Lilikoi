//       ========================
//       Lilikoi.Benchmarks::Hell.cs
//       Distributed under the MIT License.
//
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
//
// ->    Purpose:
//
//
//       ========================
using System.Runtime.CompilerServices;

namespace Lilikoi.Benchmarks.Mahogany.Applications.InjectSimple;

public class Simple
{

	public int Value { get; set; }

	[MethodImpl(MethodImplOptions.NoInlining)]
	public static Simple Create()
	{
		return new Simple()
		{
			 Value = 0 //Random.Shared.Next()
		};
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	public bool Execute()
	{
		return 1 == 0;  // Random.Shared.Next() == Value;
	}

}
