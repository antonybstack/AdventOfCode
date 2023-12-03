using CommunityToolkit.HighPerformance;

namespace AdventOfCode;

internal static class SpanExtensions
{
    public static int GetRowCount(this ReadOnlySpan<byte> span)
    {
        int len = ReadOnlySpanExtensions.Count(span, (byte)'\n');
        return span[^1] is not (byte)'\n' ? len + 1 : len;
    }

    public static int GetRowCount(this Span<byte> span) => GetRowCount((ReadOnlySpan<byte>)span);

    public static int GetStride(this ReadOnlySpan<byte> span) => span.IndexOf((byte)'\n');
    public static int GetStride(this Span<byte> span) => GetStride((ReadOnlySpan<byte>)span);

    public static SpanByteEnumerator Split(this ReadOnlySpan<byte> span, char seperator) =>
        new(span, seperator);

    public static SpanByteEnumerator Split(this Span<byte> span, char seperator) =>
        new(span, seperator);

    internal ref struct SpanByteEnumerator
    {
        private readonly char Seperator;
        private ReadOnlySpan<byte> Span;

        public SpanByteEnumerator(ReadOnlySpan<byte> span, char seperator)
        {
            Span = span;
            Seperator = seperator;
        }

        public ReadOnlySpan<byte> Current { get; private set; }

        public readonly SpanByteEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (Span.Length is 0)
            {
                return false;
            }
            var index = Span.IndexOf((byte)Seperator);
            if (index < 0)
            {
                Current = Span;
                Span = default;
            }
            else
            {
                Current = Span[..index];
                Span = Span[(index + 1)..];
            }
            if (Current.Length is 0)
            {
                return MoveNext();
            }
            return true;
        }
    }

    public static SpanCharEnumerator Split(this ReadOnlySpan<char> span, char seperator) =>
        new(span, seperator);

    public static SpanCharEnumerator Split(this Span<char> span, char seperator) =>
        new(span, seperator);

    internal ref struct SpanCharEnumerator
    {
        private readonly char Seperator;
        private ReadOnlySpan<char> Span;

        public SpanCharEnumerator(ReadOnlySpan<char> span, char seperator)
        {
            Span = span;
            Seperator = seperator;
        }

        public ReadOnlySpan<char> Current { get; private set; }

        public readonly SpanCharEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (Span.Length is 0)
            {
                return false;
            }
            var index = Span.IndexOf(Seperator);
            if (index < 0)
            {
                Current = Span;
                Span = default;
            }
            else
            {
                Current = Span[..index];
                Span = Span[(index + 1)..];
            }
            if (Current.Length is 0)
            {
                return MoveNext();
            }
            return true;
        }
    }
}
