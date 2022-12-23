// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;

namespace Lilikoi.Benchmarks;

public static class Program
{
	static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
}
