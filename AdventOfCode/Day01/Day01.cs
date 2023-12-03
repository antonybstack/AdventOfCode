namespace AdventOfCode.Day01;

public class Day01 : PuzzleSolver
{
    public override dynamic SolvePart1(byte[] input)
    {
        return Part1.CalibrationMachine(input);
    }

    public override dynamic SolvePart2(byte[] input)
    {
        return Part2.CalibrationMachine(input);
    }

    private static class Part1
    {
        public static decimal CalibrationMachine(Span<byte> input)
        {
            int total = 0;
            foreach (var line in input.Split('\n'))
            {
                if (TryGetEarliestNumber(line, out int n1) &&
                    TryGetLatestNumber(line, out int n2))
                {
                    total += n1 * 10 + n2;
                }
            }
            return total;
        }

        private static bool TryGetEarliestNumber(ReadOnlySpan<byte> span, out int result)
        {
            result = 0;
            for (int i = 0; i < span.Length; i++)
            {
                if (!char.IsNumber((char)span[i])) continue;
                result = span[i] - '0';
                return true;
            }
            return false;
        }

        private static bool TryGetLatestNumber(ReadOnlySpan<byte> span, out int result)
        {
            result = 0;
            for (int i = span.Length - 1; i >= 0; i--)
            {
                if (!char.IsNumber((char)span[i])) continue;
                result = span[i] - '0';
                return true;
            }
            return false;
        }
    }

    private static class Part2
    {
        public static decimal CalibrationMachine(Span<byte> input)
        {
            int total = 0;
            foreach (var line in input.Split('\n'))
            {
                if (TryGetEarliestNumber(line, out int n1) &&
                    TryGetLatestNumber(line, out int n2))
                {
                    total += n1 * 10 + n2;
                }
            }
            return total;
        }

        private static bool TryGetEarliestNumber(ReadOnlySpan<byte> span, out int result)
        {
            result = 0;
            for (int i = 0; i < span.Length; i++)
            {
                if (char.IsNumber((char)span[i]))
                {
                    result = span[i] - '0';
                    return true;
                }
                for (int size = 3; size < 6; size++)
                {
                    if (i + size > span.Length) break;
                    var word = span[i..(i + size)];
                    if (TryParseNumber(word, out result))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool TryGetLatestNumber(ReadOnlySpan<byte> span, out int result)
        {
            result = 0;
            for (int i = span.Length - 1; i >= 0; i--)
            {
                if (char.IsNumber((char)span[i]))
                {
                    result = span[i] - '0';
                    return true;
                }
                for (int size = 3; size < 6; size++)
                {
                    if (i + size > span.Length) break;
                    var word = span[i..(i + size)];
                    if (TryParseNumber(word, out result))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool TryParseNumber(ReadOnlySpan<byte> word, out int result)
        {
            result = word.Length switch
            {
                3 when word.SequenceEqual("one"u8) => 1,
                3 when word.SequenceEqual("two"u8) => 2,
                3 when word.SequenceEqual("six"u8) => 6,
                3 when word.SequenceEqual("ten"u8) => 10,
                4 when word.SequenceEqual("four"u8) => 4,
                4 when word.SequenceEqual("five"u8) => 5,
                4 when word.SequenceEqual("nine"u8) => 9,
                5 when word.SequenceEqual("three"u8) => 3,
                5 when word.SequenceEqual("seven"u8) => 7,
                5 when word.SequenceEqual("eight"u8) => 8,
                _ => 0
            };
            return result is not 0;
        }
    }
}
