//       ========================
//       Lilikoi.Benchmarks::Mount.cs
//
// ->    Created: 01.02.2023
// ->    Bumped: 01.02.2023
//
// ->    Purpose:
//
//
//       ========================
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

using Lilikoi.Attributes;
using Lilikoi.Attributes.Builders;
using Lilikoi.Attributes.Typed;
using Lilikoi.Benchmarks.Mahogany.Applications.InjectSimple;
using Lilikoi.Compiler.Public;
using Lilikoi.Compiler.Public.Utilities;
using Lilikoi.Context;

namespace Lilikoi.Benchmarks.Micro;


//[SimpleJob(RuntimeMoniker.Net47)]
[SimpleJob(RuntimeMoniker.Net48)]
//[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[Q1Column, MeanColumn, MedianColumn, Q3Column, StdDevColumn, StdErrorColumn]
[MemoryDiagnoser(true)]
//[EventPipeProfiler(EventPipeProfile.CpuSampling)]
public class MountBenchmark
{
	public Mount Mount = new Mount();

	[Params(0, 5, 15)]
	public int Fodder;

	[GlobalSetup]
	public void Setup()
	{
		Mount.Store(new MountBenchmark());

		Type[] fodder = new[]
		{
			typeof(Dictionary<String, String>),
			typeof(Dictionary<String, int>),
			typeof(Dictionary<String, long>),
			typeof(Dictionary<String, DateTime>),
			typeof(Dictionary<String, object>),
			typeof(Dictionary<String, double>),
			typeof(Dictionary<String, float>),


			typeof(List<string>),
			typeof(List<int>),
			typeof(List<long>),
			typeof(List<DateTime>),
			typeof(List<object>),
			typeof(List<double>),
			typeof(List<float>),
			typeof(List<char>),
			typeof(List<byte>),

		};

		for (int i = 0; i < Fodder; i++)
		{
			Console.WriteLine(fodder[i].FullName);
			Mount.Store(Activator.CreateInstance(fodder[i]));
		}

	}

	[Benchmark]
	public MountBenchmark Read()
	{
		return Mount.Get<MountBenchmark>();
	}

	[Benchmark]
	public bool Has()
	{
		return Mount.Has<MountBenchmark>();
	}

	[Benchmark]
	public void Store()
	{
		Mount.Store<MountBenchmark>(this);
	}
}
