namespace AdventOfCode.Day1;

public static class Part1
{
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
            if (!char.IsNumber(span[i])) continue;
            result = span[i] - '0';
            return true;
        }
        return false;
    }

    private static bool TryGetLatestNumber(ReadOnlySpan<char> span, out int result)
    {
        result = 0;
        for (int i = span.Length - 1; i >= 0; i--)
        {
            if (!char.IsNumber(span[i])) continue;
            result = span[i] - '0';
            return true;
        }
        return false;
    }
}
