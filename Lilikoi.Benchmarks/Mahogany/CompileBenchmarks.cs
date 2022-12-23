//       ========================
//       Lilikoi.Benchmarks::CompileBenchmarks.cs
//       Distributed under the MIT License.
//
// ->    Created: 22.12.2022
// ->    Bumped: 22.12.2022
//
// ->    Purpose:
//
//
//       ========================
using BenchmarkDotNet.Attributes;

using Lilikoi.Benchmarks.Mahogany.Applications.InjectSimple;
using Lilikoi.Core.Builder.Public;

namespace Lilikoi.Benchmarks.Mahogany;

[SimpleJob()]
[Q1Column, MeanColumn, MedianColumn, Q3Column]
public class CompileBenchmarks
{

	[Benchmark()]
	public Func<SimpleInjectHost, bool, bool> Simple()
	{
		return SimpleInjectHost.Build();
	}

}
