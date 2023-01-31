//       ========================
//       Lilikoi.Benchmarks::RunBenchmarks.cs
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

namespace Lilikoi.Benchmarks.Mahogany;

[SimpleJob()]
[Q1Column, MeanColumn, MedianColumn, Q3Column, StdDevColumn, StdErrorColumn]
[MemoryDiagnoser(true)]
public class RunBenchmarks
{

	public Func<SimpleInjectHost, bool, bool> SimpleContainer;
	public SimpleInjectHost SimpleHost = new SimpleInjectHost();

	[GlobalSetup]
	public void Setup()
	{
		SimpleContainer = SimpleInjectHost.Build();
	}

	[Benchmark()]
	public bool Simple()
	{
		return SimpleContainer(SimpleHost, true);
	}

}
