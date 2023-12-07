namespace AdventOfCode.Day06;

public sealed class Day06 : PuzzleSolver
{
    public override dynamic SolvePart1(byte[] input)
    {
        return Part1.Run(input);
    }

    public override dynamic SolvePart2(byte[] input)
    {
        return Part2.Run(input);
    }

    private static class Part1
    {
        public static long Run(Span<byte> input)
        {
            var timesSpan = input[(input.IndexOf((byte)':') + 1)..input.IndexOf((byte)'\n')];
            int bufferIdx = 0;
            Span<int> times = stackalloc int[4];
            foreach (var span in timesSpan.Split(' '))
            {
                times[bufferIdx++] = int.Parse(span);
            }
            var distancesSpan = input[(input.LastIndexOf((byte)':') + 1)..^1];
            Span<int> distances = stackalloc int[4];
            bufferIdx = 0;
            foreach (var span in distancesSpan.Split(' '))
            {
                distances[bufferIdx++] = int.Parse(span);
            }
            long result = 1L;
            for (int i = 0; i < bufferIdx; i++)
            {
                result *= GetCountOfWinPermutations(times[i], distances[i]);
            }
            return result;
        }
    }

    private static class Part2
    {
        public static long Run(Span<byte> input)
        {
            var timesSpan = input[(input.IndexOf((byte)':') + 1)..input.IndexOf((byte)'\n')];
            long time = 0L;
            foreach (var span in timesSpan)
            {
                if (span is (byte)' ') continue;
                time = time * 10 + (span - (byte)'0');
            }
            var distancesSpan = input[(input.LastIndexOf((byte)':') + 1)..^1];
            long dist = 0L;
            foreach (var span in distancesSpan)
            {
                if (span is (byte)' ') continue;
                dist = dist * 10 + (span - (byte)'0');
            }
            return GetCountOfWinPermutations(time, dist);
        }
    }

    private static long GetCountOfWinPermutations(long time, long dist)
    {
        long wins = 0;
        for (long i = 0; i < time; i++)
        {
            long timeRemaining = time - i;
            long distCovered = i * timeRemaining;
            if (distCovered > dist)
            {
                wins++;
            }
        }
        return wins;
    }
}
