//       ========================
//       Lilikoi.Benchmarks::HeadlessInjectBenchmark.cs
//       Distributed under the MIT License.
//
// ->    Created: 23.12.2022
// ->    Bumped: 23.12.2022
//
// ->    Purpose:
//
//
//       ========================
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Jobs;

using Lilikoi.Benchmarks.Mahogany.Applications.InjectSimple;
using Lilikoi.Core.Builder.Public.Utilities;

using Perfolizer.Mathematics.QuantileEstimators;

namespace Lilikoi.Benchmarks.Mahogany;

[SimpleJob(RuntimeMoniker.Net462)]
[SimpleJob(RuntimeMoniker.Net47)]
[SimpleJob(RuntimeMoniker.Net48)]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[Q1Column, MeanColumn, MedianColumn, Q3Column, StdDevColumn, StdErrorColumn]
[MemoryDiagnoser(true)]
//[EventPipeProfiler(EventPipeProfile.CpuSampling)]
public class HeadlessInjectBenchmark
{
	public SimpleInjectHost SimpleHost = new SimpleInjectHost();
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
