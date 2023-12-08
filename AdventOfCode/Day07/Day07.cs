namespace AdventOfCode.Day07;

public sealed class Day07 : PuzzleSolver
{
    public override dynamic SolvePart1(byte[] input)
    {
        return Part1.Run(input);
    }

    public override dynamic SolvePart2(byte[] input)
    {
        return Part2.Run(input);
    }

    private static class Part1
    {
        public static int Run(Span<byte> input)
        {
            Span<byte> cardCounts = stackalloc byte[16];
            int numHands = input.Count((byte)'\n');
            ulong[] allHandsBitmask = new ulong[numHands];
            for (int handIdx = 0; handIdx < numHands; handIdx++)
            {
                cardCounts.Clear();
                // bitmask of the hand's cards
                // e.g. 32T3K => 110010101000111101
                int handRawValueBitmask = 0;
                // track the max of a kind (Five of a kind, Four of a kind, Three of a kind, Two of a kind)
                int maxCombo = 1;
                int uniqueCards = 0;
                for (int cardIdx = 0; cardIdx < 5; cardIdx++)
                {
                    byte card = input[cardIdx];
                    byte cardValue = card switch
                    {
                        (byte)'A' => 14,
                        (byte)'K' => 13,
                        (byte)'Q' => 12,
                        (byte)'J' => 11,
                        (byte)'T' => 10,
                        _ => (byte)(card - '0')
                    };
                    int offset = (4 - cardIdx) * 4;
                    handRawValueBitmask |= cardValue << offset;
                    var cnt = ++cardCounts[cardValue];
                    maxCombo = Math.Max(maxCombo, cnt);
                    if (cnt is 1) uniqueCards++;
                }
                // score based on the max of any kind; remove the number of unique cards
                var score = GetHandScore(maxCombo, uniqueCards);
                // parse the bid value
                uint bid = 0;
                var bidIdx = 6;
                while (input[bidIdx] is not (byte)'\n')
                {
                    uint digit = input[bidIdx++];
                    bid = bid * 10 + (digit - '0');
                }
                input = input[++bidIdx..]; // next hand
                // store the hand's score and raw value in first 32 bits of bitmask; bid in the last 32 bits
                allHandsBitmask[handIdx] = ((ulong)score << 52) | ((ulong)handRawValueBitmask << 32) | bid;
            }
            Array.Sort(allHandsBitmask);
            int output = 0;
            for (int i = 0; i < allHandsBitmask.Length; i++)
            {
                // take the bid value of long bitmask and multiply it by the hand's index
                output += (int)(allHandsBitmask[i] & int.MaxValue) * (i + 1);
            }
            return output;

            static int GetHandScore(int maxOfAKind, int numCards)
            {
                return (maxOfAKind, numCards) switch
                {
                    (5, _) => 8, // Five of a kind
                    (4, _) => 6, // Four of a kind
                    (3, 2) => 5, // Full house
                    (3, _) => 4, // Three of a kind
                    (2, 3) => 3, // Two pair
                    (2, _) => 2, // One pair
                    _ => 0 // High card
                };
            }
        }
    }

    private static class Part2
    {
        public static int Run(Span<byte> input)
        {
            Span<byte> cardCounts = stackalloc byte[16];
            int handsCount = input.Count((byte)'\n');
            ulong[] allHandsBitmask = new ulong[handsCount];
            for (ushort handIndex = 0; handIndex < handsCount; handIndex++)
            {
                cardCounts.Clear();
                int handRawValueBitmask = 0;
                byte maxCombo = 0;
                byte numCards = 0;
                for (int cardIdx = 0; cardIdx < 5; cardIdx++)
                {
                    byte card = input[cardIdx];
                    byte cardValue = card switch
                    {
                        (byte)'A' => 14,
                        (byte)'K' => 13,
                        (byte)'Q' => 12,
                        (byte)'J' => 11,
                        (byte)'T' => 10,
                        _ => (byte)(card - '0')
                    };
                    byte newCount = ++cardCounts[cardValue];
                    if (newCount == 1)
                        numCards++;
                    if (card is (byte)'J')
                        cardValue = 1;
                    else if (newCount > maxCombo)
                        maxCombo = newCount;
                    int offset = (4 - cardIdx) * 4;
                    handRawValueBitmask |= cardValue << offset;
                }
                byte jokers = cardCounts[11]; // wildcards
                int score;
                if (jokers is not 0)
                {
                    // use joker(s) as wildcards to complete the hand
                    score = maxCombo + jokers + 4 - Math.Max(1, numCards - 1);
                }
                else
                {
                    score = Math.Max(jokers, maxCombo) + 4 - numCards;
                }
                uint bid = 0;
                int bidIdx = 6;
                while (input[bidIdx] is not (byte)'\n')
                {
                    uint digit = input[bidIdx++];
                    bid = bid * 10 + (digit - '0');
                }
                input = input[++bidIdx..];
                allHandsBitmask[handIndex] = ((ulong)score << 52) | ((ulong)handRawValueBitmask << 32) | bid;
            }
            Array.Sort(allHandsBitmask);
            int result = 0;
            for (int i = 0; i < allHandsBitmask.Length; i++)
            {
                result += (int)(allHandsBitmask[i] & int.MaxValue) * (i + 1);
            }
            return result;
        }
    }
}
