namespace AdventOfCode;

public abstract class PuzzleSolver
{
    public int Day => int.Parse(GetType().Name[3..]);
    public abstract dynamic SolvePart1(byte[] input);
    public abstract dynamic SolvePart2(byte[] input);
}
