using System.Text;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day23
{
    public static class GameBoardLarge
    {
        private static readonly char[] expectedResult = new[] { 'A', 'A', 'A', 'A', 'B', 'B', 'B', 'B', 'C', 'C', 'C', 'C', 'D', 'D', 'D', 'D' };
        internal static bool IsCorrect(char[] items)
        {
            return items.AsSpan(7).SequenceEqual(expectedResult);
        }

        public const char EMPTY = '.';

        public static string AsString(ReadOnlySpan<char> items)
        {
            var sb = new StringBuilder()
                .AppendLine()
                .AppendLine("#############")
                .Append('#')
                .Append(items[0])
                .Append(items[1])
                .Append('.')
                .Append(items[2])
                .Append('.')
                .Append(items[3])
                .Append('.')
                .Append(items[4])
                .Append('.')
                .Append(items[5])
                .Append(items[6])
                .AppendLine("#");
            for (var i = 0; i < 4; i++)
            {
                sb.Append(i is 0 ? "###" : "  #")
                    .Append(items[i + 7])
                    .Append('#')
                    .Append(items[i + 11])
                    .Append('#')
                    .Append(items[i + 15])
                    .Append('#')
                    .Append(items[i + 19])
                    .AppendLine(i is 0 ? "###" : "#");
            }
            sb.AppendLine("  #########");
            return sb.ToString();
        }

        public static IEnumerable<(char[] board, long score)> AllOptions(ReadOnlySpan<char> items)
        {
            return Iterator(items);
        }

        private static IEnumerable<(char[] board, long score)> Iterator(ReadOnlySpan<char> items)
        {
            var list = new List<(char[] board, long score)>();
            (char[] board, long score) result;
            for (var i = 1; i <= 4; i++)
            {
                var column = ColumnByNumber(items, i);
                var letterForColumn = (char)(i + 64);
                if (IsColumnFull(column) && column[0] == letterForColumn && column[1] == letterForColumn)
                {
                    continue;
                }
                if (!IsColumnFull(column))
                {
                    result = MoveItemInFromLeft(items, column, i);
                    if (result.score != 0)
                    {
                        list.Add(result);
                    }
                    result = MoveItemInFromRight(items, column, i);
                    if (result.score != 0)
                    {
                        list.Add(result);
                    }
                }

                if (!IsColumnEmpty(column))
                {
                    if (column[0] == EMPTY && column[1] == letterForColumn)
                    {
                        continue;
                    }
                    result = MoveColumnToLeft(items, column, i);
                    if (result.score != 0)
                    {
                        list.Add(result);
                        for (var j = i - 1; j >= 0; j--)
                        {
                            if (result.board[j] != EMPTY)
                            {
                                break;
                            }
                            var copy = result.board.ToArray();
                            copy[j] = copy[i];
                            copy[i] = EMPTY;
                            list.Add((copy, result.score + StepValue(copy[j], (i - j) * 2 - (j is 0 ? 1 : 0))));
                        }
                    }
                    result = MoveColumnToRight(items, column, i);
                    if (result.score != 0)
                    {
                        list.Add(result);
                        for (var j = i + 2; j <= 6; j++)
                        {
                            if (result.board[j] != EMPTY)
                            {
                                break;
                            }
                            var copy = result.board.ToArray();
                            copy[j] = copy[i + 1];
                            copy[i + 1] = EMPTY;
                            list.Add((copy, result.score + StepValue(copy[j], (j - i - 1) * 2 - (j is 6 ? 1 : 0))));
                        }
                    }
                }
            }

            //for (var i = 2; i <= 4; i++)
            //{
            //    result = MoveMiddleItemLeft(items, i);
            //    if (result.score != 0)
            //    {
            //        list.Add(result);
            //    }
            //    result = MoveMiddleItemRight(items, i);
            //    if (result.score != 0)
            //    {
            //        list.Add(result);
            //    }
            //}
            return list;
        }

        public static (char[] board, long score) MoveMiddleItemLeft(ReadOnlySpan<char> items, int index)
        {
            var boardAsArray = items.ToArray();
            var valueAtIndex = boardAsArray[index];
            if (valueAtIndex == EMPTY || boardAsArray.Take(index).All(x => x != EMPTY))
            {
                return (Array.Empty<char>(), 0);
            }
            boardAsArray[index] = EMPTY;
            var energyCost = InsertAt(valueAtIndex, index - 1);
            return (boardAsArray, energyCost);

            long InsertAt(char c, int at)
            {
                var currentValue = boardAsArray[at];
                var energyCost = StepValue(c, at == 0 ? 1 : 2);
                if (currentValue == EMPTY)
                {
                    boardAsArray[at] = c;
                    return energyCost;
                }
                energyCost += InsertAt(currentValue, at - 1);
                boardAsArray[at] = c;
                return energyCost;
            }
        }

        public static (char[] board, long score) MoveMiddleItemRight(ReadOnlySpan<char> items, int index)
        {
            var boardAsArray = items.ToArray();
            var valueAtIndex = boardAsArray[index];
            if (valueAtIndex == EMPTY || boardAsArray.Take(7).Skip(index + 1).All(x => x != EMPTY))
            {
                return (Array.Empty<char>(), 0);
            }
            boardAsArray[index] = EMPTY;
            var energyCost = InsertAt(valueAtIndex, index + 1);
            return (boardAsArray, energyCost);

            long InsertAt(char c, int at)
            {
                var currentValue = boardAsArray[at];
                var energyCost = StepValue(c, at == 6 ? 1 : 2);
                if (currentValue == EMPTY)
                {
                    boardAsArray[at] = c;
                    return energyCost;
                }
                energyCost += InsertAt(currentValue, at + 1);
                boardAsArray[at] = c;
                return energyCost;
            }
        }

        public static (char[] board, long score) MoveItemInFromLeft(ReadOnlySpan<char> items, int column)
        {
            var insertColumn = ColumnByNumber(items, column);
            return MoveItemInFromLeft(items, insertColumn, column);
        }

        public static (char[] board, long score) MoveItemInFromRight(ReadOnlySpan<char> items, int column)
        {
            var insertColumn = ColumnByNumber(items, column);
            return MoveItemInFromRight(items, insertColumn, column);
        }

        private static (char[] board, long score) MoveItemInFromLeft(ReadOnlySpan<char> items, ReadOnlySpan<char> insertColumn, int column)
        {
            var indexOfItemToRemove = column;
            while (items[indexOfItemToRemove] == EMPTY)
            {
                indexOfItemToRemove--;
                if (indexOfItemToRemove < 0)
                {
                    return (Array.Empty<char>(), 0);
                }
            }

            var itemToRemove = items[indexOfItemToRemove];
            if (itemToRemove - 64 != column)
            {
                return (Array.Empty<char>(), 0);
            }
            if (!EmptyOrContainsOnly(insertColumn, itemToRemove))
            {
                return (Array.Empty<char>(), 0);
            }

            var asArray = items.ToArray();
            asArray[indexOfItemToRemove] = EMPTY;
            var depth = 0;
            // attempt fourth, then third, then second then first position
            for (var i = 3; i >= 0; i--)
            {
                var arrayIndex = column * 4 + 3 + i;
                var item = asArray[arrayIndex];
                if (item == EMPTY)
                {
                    depth = i;
                    asArray[arrayIndex] = itemToRemove;
                    break;
                }
            }

            var positionsWalked = (column - indexOfItemToRemove) * 2 + 1 + 1 + depth;
            if (indexOfItemToRemove == 0)
            {
                positionsWalked--;
            }
            var energy = StepValue(itemToRemove, positionsWalked);
            return (asArray, energy);
        }

        public static (char[] board, long score) MoveItemInFromRight(ReadOnlySpan<char> items, ReadOnlySpan<char> insertColumn, int column)
        {
            var indexOfItemToRemove = column + 1;
            while (items[indexOfItemToRemove] == EMPTY)
            {
                indexOfItemToRemove++;
                if (indexOfItemToRemove > 6)
                {
                    return (Array.Empty<char>(), 0);
                }
            }

            var itemToRemove = items[indexOfItemToRemove];
            if (itemToRemove - 64 != column)
            {
                return (Array.Empty<char>(), 0);
            }

            if (!EmptyOrContainsOnly(insertColumn, itemToRemove))
            {
                return (Array.Empty<char>(), 0);
            }

            var asArray = items.ToArray();
            asArray[indexOfItemToRemove] = EMPTY;
            var depth = 0;
            // attempt second then first position
            for (var i = 3; i >= 0; i--)
            {
                var arrayIndex = column * 4 + 3 + i;
                var item = asArray[arrayIndex];
                if (item == EMPTY)
                {
                    depth = i;
                    asArray[arrayIndex] = itemToRemove;
                    break;
                }
            }

            var positionsWalked = (indexOfItemToRemove - column - 1) * 2 + 1 + 1 + depth;
            if (indexOfItemToRemove == 6)
            {
                positionsWalked--;
            }
            var energy = StepValue(itemToRemove, positionsWalked);
            return (asArray, energy);
        }

        public static (char[] board, long score) MoveColumnToLeft(ReadOnlySpan<char> items, int column)
        {
            var extractColumn = ColumnByNumber(items, column);
            if (IsColumnEmpty(extractColumn))
            {
                return (Array.Empty<char>(), 0);
            }
            return MoveColumnToLeft(items, extractColumn, column);
        }

        private static (char[] board, long score) MoveColumnToLeft(ReadOnlySpan<char> items, ReadOnlySpan<char> extractColumn, int column)
        {
            var moveIndex = -1;
            for (var i = 0; i < 4; i++)
            {
                if (extractColumn[i] != EMPTY)
                {
                    moveIndex = i;
                    break;
                }
            }
            if (moveIndex == -1)
            {
                return (Array.Empty<char>(), 0);
            }
            var extractValue = extractColumn[moveIndex];
            var extractEnergyCost = StepValue(extractValue, 2 + moveIndex);
            var insertIndex = column;
            if (items[insertIndex] != EMPTY)
            {
                return (Array.Empty<char>(), 0);
            }

            var boardAsArray = items.ToArray();
            boardAsArray[column * 4 + 3 + moveIndex] = EMPTY;

            InsertAt(extractValue, insertIndex);

            return (boardAsArray, extractEnergyCost);

            void InsertAt(char c, int at)
            {
                var currentValue = boardAsArray[at];
                if (currentValue == EMPTY)
                {
                    boardAsArray[at] = c;
                    return;
                }
                extractEnergyCost += StepValue(currentValue, at is 1 ? 1 : 2);
                InsertAt(currentValue, at - 1);
                boardAsArray[at] = c;
            }
        }

        private static long StepValue(char c, int steps)
        {
            return (long)(Math.Pow(10, c - 65)) * steps;
        }

        private static ReadOnlySpan<char> ColumnByNumber(ReadOnlySpan<char> items, int columnNr)
        {
            return items.Slice(columnNr * 4 + 3, 4);
        }

        private static bool EmptyOrContainsOnly(in ReadOnlySpan<char> column, char c)
        {
            return (column[0] == c || column[0] == EMPTY)
                && (column[1] == c || column[1] == EMPTY)
                && (column[2] == c || column[2] == EMPTY)
                && (column[3] == c || column[3] == EMPTY);
        }

        public static bool IsColumnFull(ReadOnlySpan<char> items)
        {
            return items[0] != EMPTY
                && items[1] != EMPTY
                && items[2] != EMPTY
                && items[3] != EMPTY;
        }

        public static bool IsColumnEmpty(ReadOnlySpan<char> items)
        {
            return items[0] == EMPTY
                && items[1] == EMPTY
                && items[2] == EMPTY
                && items[3] == EMPTY;
        }

        public static (char[] board, long score) MoveColumnToRight(ReadOnlySpan<char> items, int column)
        {
            var extractColumn = ColumnByNumber(items, column);
            if (IsColumnEmpty(extractColumn))
            {
                return (Array.Empty<char>(), 0);
            }
            return MoveColumnToRight(items, extractColumn, column);
        }

        private static (char[] board, long score) MoveColumnToRight(ReadOnlySpan<char> items, ReadOnlySpan<char> extractColumn, int column)
        {
            var moveIndex = -1;
            for (var i = 0; i < 4; i++)
            {
                if (extractColumn[i] != EMPTY)
                {
                    moveIndex = i;
                    break;
                }
            }
            if (moveIndex == -1)
            {
                return (Array.Empty<char>(), 0);
            }

            var insertIndex = column + 1;
            if (items[insertIndex] != EMPTY)
            {
                return (Array.Empty<char>(), 0);
            }
            var boardAsArray = items.ToArray();
            var extractValue = extractColumn[moveIndex];
            var extractEnergyCost = StepValue(extractValue, moveIndex + 2);
            boardAsArray[column * 4 + 3 + moveIndex] = EMPTY;

            InsertAt(extractValue, insertIndex);

            return (boardAsArray, extractEnergyCost);

            void InsertAt(char c, int at)
            {
                var currentValue = boardAsArray[at];
                if (currentValue == EMPTY)
                {
                    boardAsArray[at] = c;
                    return;
                }
                extractEnergyCost += StepValue(currentValue, at is 5 ? 1 : 2);
                InsertAt(currentValue, at + 1);
                boardAsArray[at] = c;
            }
        }

        public static string StringIdentifier(ReadOnlySpan<char> items)
        {
            return new string(items);
        }
    }
}