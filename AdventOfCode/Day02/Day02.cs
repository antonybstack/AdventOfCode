namespace AdventOfCode.Day02;

public class Day02 : PuzzleSolver
{
    public override dynamic SolvePart1(byte[] input)
    {
        return Part1.CubeConundrum(input);
    }

    public override dynamic SolvePart2(byte[] input)
    {
        return Part2.CubeConundrum(input);
    }

    private static class Part1
    {
        private const int RedCubeLimit = 12;
        private const int GreenCubeLimit = 13;
        private const int BlueCubeLimit = 14;

        public static int CubeConundrum(Span<byte> input)
        {
            int sum = 0;
            Span<Range> range = stackalloc Range[128];
            int startWindow = 0;
            for (int i = 0; i < input.GetRowCount(); i++)
            {
                int endWindow = startWindow + input[startWindow..].IndexOf((byte)'\n');
                var span = input[startWindow..endWindow];
                int slow = 0, fast = 0, idx = 0;
                while (fast < span.Length)
                {
                    if (span[fast] is (byte)' ')
                    {
                        range[idx++] = new Range(slow, fast);
                        slow = fast + 1;
                    }
                    fast++;
                }
                range[idx++] = new Range(slow, fast); // add the last range
                int gameId = int.Parse(span[range[1]][..^1]);
                if (ValidateGame(span, ref range, idx - 1))
                {
                    sum += gameId;
                }
                range.Clear();
                startWindow = endWindow + 1;
            }
            return sum;
        }

        private static bool ValidateGame(Span<byte> game, ref Span<Range> range, int count)
        {
            for (int i = 2; i < count; i++)
            {
                var numString = game[range[i++]];
                var color = game[range[i]][0];
                if (!int.TryParse(numString, out int num) || IsInvalidCube(color, num))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsInvalidCube(byte color, int num) =>
            (color is (byte)'r' && num > RedCubeLimit) ||
            (color is (byte)'g' && num > GreenCubeLimit) ||
            (color is (byte)'b' && num > BlueCubeLimit);
    }

    private static class Part2
    {
        public static int CubeConundrum(Span<byte> input)
        {
            int sum = 0;
            Span<Range> range = stackalloc Range[128];
            int startWindow = 0;
            for (int i = 0; i < input.GetRowCount(); i++)
            {
                int endWindow = startWindow + input[startWindow..].IndexOf((byte)'\n');
                var span = input[startWindow..endWindow];
                int slow = 0, fast = 0, idx = 0;
                while (fast < span.Length)
                {
                    if (span[fast] is (byte)' ')
                    {
                        range[idx++] = new Range(slow, fast);
                        slow = fast + 1;
                    }
                    fast++;
                }
                range[idx++] = new Range(slow, fast);
                sum += GetProductOfMaximumCubeCounts(span, ref range, idx - 1);
                range.Clear();
                startWindow = endWindow + 1;
            }
            return sum;
        }

        private static int GetProductOfMaximumCubeCounts(Span<byte> game, ref Span<Range> range, int count)
        {
            int maxRed = 0, maxGreen = 0, maxBlue = 0;
            for (int i = 2; i < count; i++)
            {
                var numString = game[range[i++]];
                var color = game[range[i]][0];
                var num = int.Parse(numString);
                switch (color)
                {
                    case (byte)'r':
                        maxRed = Math.Max(maxRed, num);
                        break;
                    case (byte)'g':
                        maxGreen = Math.Max(maxGreen, num);
                        break;
                    case (byte)'b':
                        maxBlue = Math.Max(maxBlue, num);
                        break;
                }
            }
            return maxRed * maxGreen * maxBlue;
        }
    }
}
