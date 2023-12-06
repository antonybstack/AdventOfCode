using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using Performance;
using Test;

BenchmarkRunner.Run<PuzzleBenchmarks>(
    DefaultConfig.Instance
        .WithOptions(ConfigOptions.JoinSummary)
        .AddLogger(ConsoleLogger.Unicode)
        .AddDiagnoser(MemoryDiagnoser.Default)
        .AddExporter(MarkdownExporter.Default)
        .AddExporter(MarkdownExporter.Console)
        // .WithOptions(ConfigOptions.DisableLogFile)
        .AddJob(Job.ShortRun.WithGcServer(true).WithGcConcurrent(true).WithGcForce(true)));

namespace Performance
{
    [CategoriesColumn, AllStatisticsColumn, BaselineColumn, MinColumn, Q1Column, MeanColumn, Q3Column, MaxColumn,
     MedianColumn]
    public class PuzzleBenchmarks
    {
        [Params(1)]
        public int Day { get; set; }

        private PuzzleFixture PuzzleFixture { get; set; } = null!;

        [GlobalSetup]
        public void GlobalSetup()
        {
            PuzzleFixture = new PuzzleFixture();
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public dynamic SolvePart1()
        {
            var puzzle = PuzzleFixture.GetPuzzleByDay(Day);
            return puzzle.Solver.SolvePart1(puzzle.Input);
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public dynamic SolvePart2()
        {
            var puzzle = PuzzleFixture.GetPuzzleByDay(Day);
            return puzzle.Solver.SolvePart2(puzzle.Input);
        }
    }
}
