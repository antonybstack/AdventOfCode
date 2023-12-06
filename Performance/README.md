# Benchmarking

## Running the benchmarks

``` bash
dotnet run -c Release
```

## Sample Results

```
    BenchmarkDotNet v0.13.11, macOS Ventura 13.5.2 (22G91) [Darwin 22.6.0]
    Apple M1 Pro, 1 CPU, 8 logical and 8 physical cores
    .NET SDK 8.0.100
      [Host]   : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
      ShortRun : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
    
    Job=ShortRun  Concurrent=True  Force=True  
    Server=True  IterationCount=3  LaunchCount=1  
    WarmupCount=3  
```

| Method     | Day |     Mean |     Error |   StdDev |   StdErr |      Min |       Q1 |   Median |       Q3 |      Max |     Op/s | Baseline | Allocated |
|------------|-----|---------:|----------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|---------:|----------|----------:|
| SolvePart1 | 1   | 18.26 μs |  5.575 μs | 0.306 μs | 0.176 μs | 17.92 μs | 18.14 μs | 18.36 μs | 18.43 μs | 18.50 μs | 54,765.5 | No       |      32 B |
| SolvePart2 | 1   | 70.78 μs | 10.029 μs | 0.550 μs | 0.317 μs | 70.45 μs | 70.46 μs | 70.47 μs | 70.94 μs | 71.41 μs | 14,128.6 | No       |      32 B |
