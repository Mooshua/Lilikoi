//       ========================
//       Lilikoi.Benchmarks::HeadlessInjectBenchmark.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 23.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

using Lilikoi.Benchmarks.Mahogany.Applications.InjectSimple;
using Lilikoi.Compiler.Public.Utilities;

#endregion

namespace Lilikoi.Benchmarks.Mahogany;

[SimpleJob(RuntimeMoniker.Net462)]
[SimpleJob(RuntimeMoniker.Net47)]
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
//[EventPipeProfiler(EventPipeProfile.CpuSampling)]
public class HeadlessInjectBenchmark
{
	public SimpleInjectHost SimpleHost = new();
	public LilikoiInjector.Injector<SimpleInjectHost> SimpleInjector;

	[GlobalSetup]
	public void Setup()
	{
		SimpleInjector = LilikoiInjector.CreateInjector<SimpleInjectHost>();
	}

	[Benchmark()]
	public void Simple()
	{
		SimpleInjector(SimpleHost);
	}

	[Benchmark()]
	public void SimpleCompiler()
	{
		SimpleInjector = LilikoiInjector.CreateInjector<SimpleInjectHost>();
	}
}