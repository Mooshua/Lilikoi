//       ========================
//       Lilikoi.Benchmarks::CompileBenchmarks.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using BenchmarkDotNet.Attributes;

using Lilikoi.Benchmarks.Mahogany.Applications.InjectSimple;

#endregion

namespace Lilikoi.Benchmarks.Mahogany;

[SimpleJob()]
[Q1Column]
[MeanColumn]
[MedianColumn]
[Q3Column]
public class CompileBenchmarks
{
	[Benchmark()]
	public Func<bool, bool> Simple()
	{
		return SimpleInjectHost.Build();
	}
}
