//       ========================
//       Lilikoi.Benchmarks::Program.cs
//       (c) 2023. Distributed under the MIT License
// 
// ->    Created: 22.12.2022
// ->    Bumped: 06.02.2023
//       ========================
#region

using BenchmarkDotNet.Running;

#endregion

namespace Lilikoi.Benchmarks;

public static class Program
{
	private static void Main(string[] args)
	{
		BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
	}
}