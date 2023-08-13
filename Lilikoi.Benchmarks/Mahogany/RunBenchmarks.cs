//       ========================
//       Lilikoi.Benchmarks::RunBenchmarks.cs
//       (c) 2023. Distributed under the MIT License
//
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

using Lilikoi.Benchmarks.Mahogany.Applications.InjectSimple;

#endregion

namespace Lilikoi.Benchmarks.Mahogany;

[SimpleJob(RuntimeMoniker.Net48)]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[Q1Column]
[MeanColumn]
[MedianColumn]
[Q3Column]
[StdDevColumn]
[StdErrorColumn]
[MemoryDiagnoser(true)]
public class RunBenchmarks
{
	public Func<object, object> SimpleContainer;

	[GlobalSetup]
	public void Setup()
	{
		SimpleContainer = SimpleInjectHost.Build();
	}

	[Benchmark()]
	public object Simple()
	{
		return SimpleContainer("Hello?");
	}
}
