using System.Reflection;

namespace Test;

public static class InputProvider
{
    private static readonly char[] Template =
        Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                "Inputs/Day00.txt")
            .ToCharArray();

    public static byte[] GetBytes(int day) => File.ReadAllBytes(GetFileName(day));

    private static string GetFileName(int day)
    {
        return string.Create(Template.Length,
            (day, Template),
            static (span, state) =>
            {
                state.Template.AsSpan().CopyTo(span);
                span[^6] = (char)(state.day / 10 + '0');
                span[^5] = (char)(state.day % 10 + '0');
            });
    }
}
