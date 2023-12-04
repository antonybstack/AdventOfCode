namespace AdventOfCode.Day04;

public sealed class Day04 : PuzzleSolver
{
    public override dynamic SolvePart1(byte[] input)
    {
        return Part1.Scratchcards(input);
    }

    public override dynamic SolvePart2(byte[] input)
    {
        return Part2.Scratchcards(input);
    }

    private static class Part1
    {
        public static int Scratchcards(Span<byte> input)
        {
            var result = 0;
            int winningStartIdx = input.IndexOf((byte)':') + 2;
            int playerStartIdx = input.IndexOf((byte)'|') + 2;
            foreach (var line in input.Split('\n'))
            {
                int count = GetWinCount(line[winningStartIdx..(playerStartIdx - 3)], line[playerStartIdx..]);
                if (count is 0) continue;
                result += 1 << (count - 1);
            }
            return result;
        }
    }

    private static class Part2
    {
        public static int Scratchcards(Span<byte> input)
        {
            int buffer = 11;
            var totalCount = 0;
            int winningCardIdx = input.IndexOf((byte)':') + 2;
            int playerCardIdx = input.IndexOf((byte)'|') + 2;
            var nextCardsAccumulator = new int[buffer];
            Array.Fill(nextCardsAccumulator, 1); // every card starts with 1 copy
            int bufferIdx = 0;
            foreach (var line in input.Split('\n'))
            {
                totalCount += nextCardsAccumulator[bufferIdx];
                int count = GetWinCount(line[winningCardIdx..(playerCardIdx - 3)], line[playerCardIdx..]);
                for (int i = 1; i <= count; i++)
                {
                    nextCardsAccumulator[(bufferIdx + i) % buffer] +=
                        1 * nextCardsAccumulator[bufferIdx];
                }
                nextCardsAccumulator[bufferIdx] = 1;
                bufferIdx = (bufferIdx + 1) % buffer;
            }
            return totalCount;
        }
    }

    private static int GetWinCount(ReadOnlySpan<byte> winLine, ReadOnlySpan<byte> playerLine)
    {
        Span<bool> winningNumbers = stackalloc bool[100];
        // Record the winning numbers
        for (int x = 0; x < winLine.Length - 1; x += 3)
        {
            int num = winLine[x] is (byte)' ' ? 0 : (winLine[x] - '0') * 10;
            num += winLine[x + 1] - '0';
            winningNumbers[num] = true;
        }
        // Match against the scratch card numbers
        int count = 0;
        for (int x = 0; x < playerLine.Length - 1; x += 3)
        {
            int num = playerLine[x] is (byte)' ' ? 0 : (playerLine[x] - '0') * 10;
            num += playerLine[x + 1] - '0';
            if (winningNumbers[num])
            {
                count += 1;
            }
        }
        return count;
    }
}
