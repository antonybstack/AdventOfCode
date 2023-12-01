using AdventOfCode.Day1;
using Xunit.Abstractions;

namespace Test;

public sealed class Part1Tests(ITestOutputHelper output)
{
    public static IEnumerable<object[]> TestDataPart1()
    {
        yield return new object[] { new[] { "1abc2", "pqr3stu8vwx", "a1b2c3d4e5f", "treb7uchet" }, 142 };
    }

    [Theory]
    [MemberData(nameof(TestDataPart1))]
    public void TestCasePart1(string[] input, int expected)
    {
        Assert.Equal(expected, Part1.CalibrationMachine(input));
    }

    [Fact]
    public void TestPuzzle()
    {
        decimal res = Part1.CalibrationMachine(File.ReadAllLines("Inputs/Day1_Part1.txt"));
        output.WriteLine($"{res}");
        Assert.Equal(55017, res);
    }

    public static IEnumerable<object[]> TestDataPart2()
    {
        yield return new object[]
        {
            new[]
            {
                "two1nine", "eightwothree", "abcone2threexyz", "xtwone3four", "4nineeightseven2", "zoneight234",
                "7pqrstsixteen"
            },
            281
        };
    }

    [Theory]
    [MemberData(nameof(TestDataPart2))]
    public void TestCasePart2(string[] input, int expected)
    {
        Assert.Equal(expected, Part2.CalibrationMachine(input));
    }

    [Fact]
    public void TestPuzzlePart2()
    {
        decimal res = Part2.CalibrationMachine(File.ReadAllLines("Inputs/Day1_Part2.txt"));
        output.WriteLine($"{res}");
        Assert.Equal(53539, res);
    }
}
