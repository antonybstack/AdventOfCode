using CommunityToolkit.HighPerformance;

namespace AdventOfCode.Day03;

public sealed class Day03 : PuzzleSolver
{
    public override dynamic SolvePart1(byte[] input)
    {
        ReadOnlySpan<byte> span = input;
        int stride = span.GetStride();
        int height = span.GetRowCount();
        return Part1.GearRatios(span.AsSpan2D(0, height, stride, 1));
    }

    public override dynamic SolvePart2(byte[] input)
    {
        Span<byte> span = input;
        int stride = span.GetStride();
        int height = span.GetRowCount();
        return Part2.GearRatios(span.AsSpan2D(0, height, stride, 1));
    }

    private static class Part1
    {
        private static readonly SearchValues<byte> SearchSymbols = SearchValues.Create(".0123456789"u8);

        public static int GearRatios(ReadOnlySpan2D<byte> input)
        {
            var sum = 0;
            int rows = input.Height, cols = input.Width;
            var visited = new bool[input.Width, input.Height];
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    byte current = input[r, c];
                    if (SearchSymbols.Contains(current)) continue;
                    sum += ExploreAndSum(ref input, r, c);
                }
            }
            return sum;

            int ExploreAndSum(ref ReadOnlySpan2D<byte> grid, int x, int y)
            {
                if (x < 0 || y < 0 || x >= rows || y >= cols || visited[x, y])
                {
                    return 0;
                }
                visited[x, y] = true;
                int localSum = 0;
                foreach ((int dx, int dy) in Directions)
                {
                    int newX = x + dx;
                    int newY = y + dy;
                    if (newX >= 0 &&
                        newY >= 0 &&
                        newX < rows &&
                        newY < cols &&
                        !visited[newX, newY] &&
                        grid[newX, newY] is >= (byte)'0' and <= (byte)'9')
                    {
                        localSum += FindEntireNumber(ref grid, newX, newY, ref visited);
                    }
                }
                return localSum;
            }
        }
    }

    private static class Part2
    {
        public static int GearRatios(ReadOnlySpan2D<byte> input)
        {
            var sum = 0;
            int rows = input.Height, cols = input.Width;
            var visited = new bool[input.Width, input.Height];
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    byte current = input[r, c];
                    if (current is not (byte)'*') continue;
                    sum += ExploreAndSum(ref input, r, c);
                }
            }
            return sum;

            int ExploreAndSum(ref ReadOnlySpan2D<byte> grid, int x, int y)
            {
                if (x < 0 || y < 0 || x >= rows || y >= cols || visited[x, y])
                {
                    return 0;
                }
                visited[x, y] = true;
                Span<int> nearbyNumbers = stackalloc int[2];
                int count = 0;
                foreach ((int dx, int dy) in Directions)
                {
                    int newX = x + dx;
                    int newY = y + dy;
                    if (newX >= 0 &&
                        newY >= 0 &&
                        newX < rows &&
                        newY < cols &&
                        !visited[newX, newY] &&
                        grid[newX, newY] is >= (byte)'0' and <= (byte)'9')
                    {
                        if (count is 2) return 0;
                        nearbyNumbers[count++] = FindEntireNumber(ref grid, newX, newY, ref visited);
                    }
                }
                return nearbyNumbers[0] * nearbyNumbers[1];
            }
        }
    }

    private static readonly (int x, int y)[] Directions =
    [
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0),
        (-1, -1),
        (1, 1),
        (-1, 1),
        (1, -1)
    ];

    /// <summary>
    /// Finds the remaining digits of a number at a given position.
    /// </summary>
    private static int FindEntireNumber(ref ReadOnlySpan2D<byte> grid, int x, int y, ref bool[,] visited)
    {
        var left = y;
        while (left > 0 && grid[x, left - 1] is >= (byte)'0' and <= (byte)'9')
        {
            left--;
        }
        var right = y;
        while (right < grid.Width - 1 && grid[x, right + 1] is >= (byte)'0' and <= (byte)'9')
        {
            right++;
        }
        var number = grid.GetRowSpan(x)[left..(right + 1)];
        for (int i = left; i <= right; i++)
        {
            visited[x, i] = true;
        }
        return int.Parse(number);
    }
}
