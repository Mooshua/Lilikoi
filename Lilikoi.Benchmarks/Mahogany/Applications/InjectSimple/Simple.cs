//       ========================
//       Lilikoi.Benchmarks::Simple.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using System.Runtime.CompilerServices;

#endregion

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
	public object Execute()
	{
		return "Hello, World!";
	}
}
