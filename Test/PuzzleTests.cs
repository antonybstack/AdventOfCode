namespace Test;

public sealed class PuzzleTests(PuzzleFixture fixture) : IClassFixture<PuzzleFixture>
{
    [Fact]
    public void Day01_Part1_Sample()
    {
        var puzzle = fixture.GetPuzzleByDay(1);
        var result = puzzle.Solver.SolvePart1("""
                                              1abc2
                                              pqr3stu8vwx
                                              a1b2c3d4e5f
                                              treb7uchet

                                              """u8.ToArray());
        Assert.Equal(142, result);
    }

    [Fact]
    public void Day01_Part1_Puzzle()
    {
        var puzzle = fixture.GetPuzzleByDay(1);
        var result = puzzle.Solver.SolvePart1(puzzle.Input);
        Assert.Equal(55017, result);
    }

    [Fact]
    public void Day01_Part2_Sample()
    {
        var puzzle = fixture.GetPuzzleByDay(1);
        var result = puzzle.Solver.SolvePart2("""
                                              two1nine
                                              eightwothree
                                              abcone2threexyz
                                              xtwone3four
                                              4nineeightseven2
                                              zoneight234
                                              7pqrstsixteen

                                              """u8.ToArray());
        Assert.Equal(281, result);
    }

    [Fact]
    public void Day01_Part2_Puzzle()
    {
        var puzzle = fixture.GetPuzzleByDay(1);
        var result = puzzle.Solver.SolvePart2(puzzle.Input);
        Assert.Equal(53539, result);
    }

    private static readonly byte[] Day02SampleInput = """
                                                      Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                                                      Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                                                      Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                                                      Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                                                      Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green

                                                      """u8.ToArray();
}
