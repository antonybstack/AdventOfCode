using System.Text;
using MemoryExtensions = System.MemoryExtensions;

namespace AdventOfCode.Day05;

public sealed class Day05 : PuzzleSolver
{
    public override dynamic SolvePart1(byte[] input)
    {
        return Part1.Solve(input);
    }

    public override dynamic SolvePart2(byte[] input)
    {
        return Part2.Solve(input);
    }

    private static class Part1
    {
        public static long Solve(Span<byte> span)
        {
            int seedsStartIdx = span.IndexOf((byte)':') + 2;
            int seedsEndIdx = span.IndexOf((byte)'\n');
            var seedLine = span[seedsStartIdx..seedsEndIdx];
            int seedCount = MemoryExtensions.Count(seedLine, (byte)' ') + 1;
            Span<long> seeds = stackalloc long[seedCount];
            int seedIdx = 0;
            foreach (var line in seedLine.Split(' '))
            {
                seeds[seedIdx++] = long.Parse(line);
            }
            var mapLines = span[(seedsEndIdx + 1)..];
            Span<(long from, long length, long adjust)> rangesCollective =
                stackalloc (long from, long length, long adjust)[mapLines.Length];
            Span<int> rangeSections = stackalloc int[8];
            var rsIdx = 0;
            int rcIdx = 0;
            foreach (var mapLine in mapLines.Split(':'))
            {
                // skip the first line
                if (mapLine[1] < '0' || mapLine[1] > '9')
                    continue;
                rangeSections[rcIdx] = rsIdx;
                foreach (var rangeLine in mapLine.Split('\n'))
                {
                    if (rangeLine[0] < '0' || rangeLine[0] > '9')
                    {
                        break;
                    }
                    (int p1, int p2) = (rangeLine.IndexOf((byte)' '), rangeLine.LastIndexOf((byte)' '));
                    var dest = rangeLine[..p1];
                    var from = rangeLine[(p1 + 1)..p2];
                    var length = rangeLine[(p2 + 1)..];
                    rangesCollective[rsIdx++] = (long.Parse(from), long.Parse(from) + long.Parse(length) - 1,
                        long.Parse(dest) - long.Parse(from));
                }
                rangesCollective[rangeSections[rcIdx]..rsIdx].Sort();
                rcIdx++;
            }
            rangeSections[rcIdx] = rsIdx;
            var lowestLocation = long.MaxValue;
            foreach (var seed in seeds)
            {
                var s = seed;
                for (rcIdx = 0; rcIdx < 7; rcIdx++)
                {
                    for (var j = rangeSections[rcIdx]; j < rangeSections[rcIdx + 1]; j++)
                    {
                        (long from, long to, long adjust) = rangesCollective[j];
                        if (s >= from && s <= to)
                        {
                            s += adjust;
                            break;
                        }
                    }
                }
                if (s < lowestLocation)
                {
                    lowestLocation = s;
                }
            }
            return lowestLocation;
        }
    }

    private static class Part2
    {
        private sealed class SeedRange(long value, long length, int level)
        {
            public long Value = value;
            public long Length = length;
            public int Level = level;
        }

        public static long Solve(Span<byte> inputBytes)
        {
            ReadOnlySpan<char> span = Encoding.UTF8.GetString(inputBytes);
            var spanEnumerator = span.EnumerateLines();
            spanEnumerator.MoveNext();
            var seedLine = spanEnumerator.Current;
            List<SeedRange> seeds = ParseSeeds(seedLine[7..]);
            for (int level = 0; level <= 7; level++)
            {
                spanEnumerator = MapDetailByLevel(spanEnumerator, level);
            }
            long minLocation = long.MaxValue;
            for (int i = 0; i < seeds.Count; i++)
            {
                minLocation = Math.Min(minLocation, seeds[i].Value);
            }
            return minLocation;

            static List<SeedRange> ParseSeeds(ReadOnlySpan<char> line)
            {
                var count = line.Count(' ');
                var numbers = new long[count + 1];
                int i = 0;
                foreach (var num in line.Split(' '))
                {
                    numbers[i++] = long.Parse(num);
                }
                var seeds = new List<SeedRange>(numbers.Length / 2);
                for (i = 0; i < numbers.Length; i += 2)
                {
                    seeds.Add(new SeedRange(
                        value: numbers[i],
                        length: numbers[i + 1],
                        level: 0)); // All seeds start at level 0
                }
                return seeds;
            }

            SpanLineEnumerator MapDetailByLevel(SpanLineEnumerator spanEnumerator,
                int level)
            {
                while (spanEnumerator.MoveNext())
                {
                    while (spanEnumerator.Current.Length is 0 && spanEnumerator.MoveNext()) { }
                    var line = spanEnumerator.Current;
                    if (line.Length is 0 || line[0] is '\n' || line[^1] is ':')
                    {
                        return spanEnumerator;
                    }
                    var enumerator = line.Split(' ');
                    enumerator.MoveNext();
                    long destStart = long.Parse(enumerator.Current);
                    enumerator.MoveNext();
                    long sourceStart = long.Parse(enumerator.Current);
                    enumerator.MoveNext();
                    long length = long.Parse(enumerator.Current);
                    MapDetails(ref seeds,
                        level,
                        destStart,
                        sourceStart,
                        length);
                }
                return spanEnumerator;
            }

            static void MapDetails(ref List<SeedRange> seedsToMap,
                int level,
                long destStart,
                long sourceStart,
                long length)
            {
                for (int i = 0; i < seedsToMap.Count; i++)
                {
                    SeedRange seedRange = seedsToMap[i];
                    // Check intersection
                    if (seedRange.Level > level ||
                        seedRange.Value >= sourceStart + length ||
                        seedRange.Value + seedRange.Length <= sourceStart)
                    {
                        continue;
                    }

                    // If seed range extends beyond mapping range
                    if (seedRange.Value + seedRange.Length > sourceStart + length)
                    {
                        long newLength = seedRange.Value + seedRange.Length - sourceStart - length;
                        SeedRange newSeedRange = new SeedRange(sourceStart + length, newLength, level);
                        seedsToMap.Add(newSeedRange);
                        seedRange.Length -= newLength;
                    }

                    // If seed range extends before mapping range
                    if (seedRange.Value < sourceStart)
                    {
                        long newLength = sourceStart - seedRange.Value;
                        SeedRange newSeedRange = new SeedRange(seedRange.Value, newLength, level);
                        seedsToMap.Add(newSeedRange);
                        seedRange.Value += newLength;
                        seedRange.Length -= newLength;
                    }

                    // Change seeds value to new mapping value
                    seedRange.Value += destStart - sourceStart;
                    seedRange.Level = level + 1;
                }
            }
        }
    }
}
