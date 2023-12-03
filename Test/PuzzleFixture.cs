using System.Reflection;
using AdventOfCode;

namespace Test;

public sealed class PuzzleFixture
{
    private Puzzle[] Puzzles { get; }

    public PuzzleFixture()
    {
        var types = Assembly.GetAssembly(typeof(PuzzleSolver))?.GetTypes();
        var concretePuzzleTypes =
            types?.Where(static t => t.IsSubclassOf(typeof(PuzzleSolver))).ToArray() ?? Array.Empty<Type>();
        ArgumentNullException.ThrowIfNull(concretePuzzleTypes);
        Puzzles = new Puzzle[concretePuzzleTypes.Length + 1];
        foreach (var t in concretePuzzleTypes)
        {
            if (Activator.CreateInstance(t) is not PuzzleSolver puzzleSolver) continue;
            Puzzles[puzzleSolver.Day] = new Puzzle(puzzleSolver);
        }
    }

    public Puzzle GetPuzzleByDay(int day) => Puzzles[day];

    public sealed class Puzzle(PuzzleSolver solver)
    {
        public PuzzleSolver Solver { get; } = solver;
        public byte[] Input { get; } = InputProvider.GetBytes(solver.Day);
    }
}
