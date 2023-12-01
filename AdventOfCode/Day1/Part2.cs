namespace AdventOfCode.Day1;

public static class Part2
{
    private static readonly Dictionary<string, int> WordsSet = new()
    {
        ["one"] = 1,
        ["two"] = 2,
        ["three"] = 3,
        ["four"] = 4,
        ["five"] = 5,
        ["six"] = 6,
        ["seven"] = 7,
        ["eight"] = 8,
        ["nine"] = 9
    };

    public static decimal CalibrationMachine(Span<string> input)
    {
        int total = 0;
        foreach (string line in input)
        {
            var span = line.AsSpan();
            if (TryGetEarliestNumber(span, out int n1) &&
                TryGetLatestNumber(span, out int n2))
            {
                total += n1 * 10 + n2;
            }
        }
        return total;
    }

    private static bool TryGetEarliestNumber(ReadOnlySpan<char> span, out int result)
    {
        result = 0;
        for (int i = 0; i < span.Length; i++)
        {
            if (char.IsNumber(span[i]))
            {
                result = span[i] - '0';
                return true;
            }
            for (int size = 3; size < 6; size++)
            {
                if (i + size > span.Length) break;
                if (!WordsSet.TryGetValue(span[i..(i + size)].ToString(), out int val))
                {
                    continue;
                }
                result = val;
                return true;
            }
        }
        return false;
    }

    private static bool TryGetLatestNumber(ReadOnlySpan<char> span, out int result)
    {
        result = 0;
        for (int i = span.Length - 1; i >= 0; i--)
        {
            if (char.IsNumber(span[i]))
            {
                result = span[i] - '0';
                return true;
            }
            for (int size = 3; size < 6; size++)
            {
                if (i + size > span.Length) break;
                if (!WordsSet.TryGetValue(span[i..(i + size)].ToString(), out int val))
                {
                    continue;
                }
                result = val;
                return true;
            }
        }
        return false;
    }
}
