﻿//       ========================
//       Lilikoi.Benchmarks::Mount.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 01.02.2023
// ->    Bumped: 06.02.2023
//       ========================
#region

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

using Lilikoi.Context;

#endregion

namespace Lilikoi.Benchmarks.Micro;

//[SimpleJob(RuntimeMoniker.Net47)]
[SimpleJob(RuntimeMoniker.Net48)]
//[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[Q1Column]
[MeanColumn]
[MedianColumn]
[Q3Column]
[StdDevColumn]
[StdErrorColumn]
[MemoryDiagnoser(true)]
//[EventPipeProfiler(EventPipeProfile.CpuSampling)]
public class MountBenchmark
{
	[Params(0, 5, 15)] public int Fodder;
	public Mount Mount = new();

	[GlobalSetup]
	public void Setup()
	{
		Mount.Store(new MountBenchmark());

		Type[] fodder = new[]
		{
			typeof(Dictionary<string, string>),
			typeof(Dictionary<string, int>),
			typeof(Dictionary<string, long>),
			typeof(Dictionary<string, DateTime>),
			typeof(Dictionary<string, object>),
			typeof(Dictionary<string, double>),
			typeof(Dictionary<string, float>),


			typeof(List<string>),
			typeof(List<int>),
			typeof(List<long>),
			typeof(List<DateTime>),
			typeof(List<object>),
			typeof(List<double>),
			typeof(List<float>),
			typeof(List<char>),
			typeof(List<byte>)
		};

		for (var i = 0; i < Fodder; i++)
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