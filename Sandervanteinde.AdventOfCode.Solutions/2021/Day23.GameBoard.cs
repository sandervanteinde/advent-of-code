using System.Text;

namespace Sandervanteinde.AdventOfCode.Solutions._2021;

internal partial class Day23
{
    public static class GameBoard
    {
        public const char EMPTY = '.';

        internal static bool IsCorrect(char[] items)
        {
            return items[7] == 'A' && items[8] == 'A'
                && items[9] == 'B' && items[10] == 'B'
                && items[11] == 'C' && items[12] == 'C'
                && items[13] == 'D' && items[14] == 'D';
        }

        public static string AsString(ReadOnlySpan<char> items)
        {
            var sb = new StringBuilder()
                .AppendLine()
                .AppendLine("#############")
                .Append(value: '#')
                .Append(items[index: 0])
                .Append(items[index: 1])
                .Append(value: '.')
                .Append(items[index: 2])
                .Append(value: '.')
                .Append(items[index: 3])
                .Append(value: '.')
                .Append(items[index: 4])
                .Append(value: '.')
                .Append(items[index: 5])
                .Append(items[index: 6])
                .AppendLine("#")
                .Append("###")
                .Append(items[index: 7])
                .Append(value: '#')
                .Append(items[index: 9])
                .Append(value: '#')
                .Append(items[index: 11])
                .Append(value: '#')
                .Append(items[index: 13])
                .AppendLine("###")
                .Append("  #")
                .Append(items[index: 8])
                .Append(value: '#')
                .Append(items[index: 10])
                .Append(value: '#')
                .Append(items[index: 12])
                .Append(value: '#')
                .Append(items[index: 14])
                .AppendLine("#")
                .AppendLine("  #########");
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

                if (IsColumnFull(column) && column[index: 0] == letterForColumn && column[index: 1] == letterForColumn)
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

                if (!column.IsEmpty)
                {
                    if (column[index: 0] == EMPTY
                        && (column[index: 1] == letterForColumn || column[index: 1] == EMPTY)
                        && (column[index: 2] == letterForColumn || column[index: 2] == EMPTY)
                        && column[index: 3] == letterForColumn
                       )
                    {
                        continue;
                    }

                    result = MoveColumnToLeft(items, column, i);

                    if (result.score != 0)
                    {
                        list.Add(result);
                    }

                    result = MoveColumnToRight(items, column, i);

                    if (result.score != 0)
                    {
                        list.Add(result);
                    }
                }
            }

            for (var i = 2; i <= 4; i++)
            {
                result = MoveMiddleItemLeft(items, i);

                if (result.score != 0)
                {
                    list.Add(result);
                }

                result = MoveMiddleItemRight(items, i);

                if (result.score != 0)
                {
                    list.Add(result);
                }
            }

            return list;
        }

        public static (char[] board, long score) MoveMiddleItemLeft(ReadOnlySpan<char> items, int index)
        {
            var boardAsArray = items.ToArray();
            var valueAtIndex = boardAsArray[index];

            if (valueAtIndex == EMPTY || boardAsArray.Take(index)
                    .All(x => x != EMPTY))
            {
                return (Array.Empty<char>(), 0);
            }

            boardAsArray[index] = EMPTY;
            var energyCost = InsertAt(valueAtIndex, index - 1);
            return (boardAsArray, energyCost);

            long InsertAt(char c, int at)
            {
                var currentValue = boardAsArray[at];
                var energyCost = StepValue(
                    c, at == 0
                        ? 1
                        : 2
                );

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

            if (valueAtIndex == EMPTY || boardAsArray.Take(count: 7)
                    .Skip(index + 1)
                    .All(x => x != EMPTY))
            {
                return (Array.Empty<char>(), 0);
            }

            boardAsArray[index] = EMPTY;
            var energyCost = InsertAt(valueAtIndex, index + 1);
            return (boardAsArray, energyCost);

            long InsertAt(char c, int at)
            {
                var currentValue = boardAsArray[at];
                var energyCost = StepValue(
                    c, at == 6
                        ? 1
                        : 2
                );

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

            // attempt second then first position
            for (var i = 1; i >= 0; i--)
            {
                var arrayIndex = (column * 2) + 5 + i;
                var item = asArray[arrayIndex];

                if (item == EMPTY)
                {
                    depth = i;
                    asArray[arrayIndex] = itemToRemove;
                    break;
                }
            }

            var positionsWalked = ((column - indexOfItemToRemove) * 2) + 1 + 1 + depth;

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

            if (!EmptyOrContainsOnly(items, itemToRemove))
            {
                return (Array.Empty<char>(), 0);
            }

            var asArray = items.ToArray();
            asArray[indexOfItemToRemove] = EMPTY;
            var depth = 0;

            // attempt second then first position
            for (var i = 1; i >= 0; i--)
            {
                var arrayIndex = (column * 2) + 5 + i;
                var item = asArray[arrayIndex];

                if (item == EMPTY)
                {
                    depth = i;
                    asArray[arrayIndex] = itemToRemove;
                    break;
                }
            }

            var positionsWalked = ((indexOfItemToRemove - column - 1) * 2) + 1 + 1 + depth;

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

            if (extractColumn.IsEmpty)
            {
                return (Array.Empty<char>(), 0);
            }

            return MoveColumnToLeft(items, extractColumn, column);
        }

        private static (char[] board, long score) MoveColumnToLeft(ReadOnlySpan<char> items, ReadOnlySpan<char> extractColumn, int column)
        {
            var moveIndex = extractColumn[index: 0] != EMPTY
                ? 0
                : extractColumn[index: 1] != EMPTY
                    ? 1
                    : -1;

            if (moveIndex == -1)
            {
                return (Array.Empty<char>(), 0);
            }

            var extractValue = extractColumn[moveIndex];
            var extractEnergyCost = StepValue(
                extractValue, moveIndex == 0
                    ? 2
                    : 3
            );
            var insertIndex = column;
            var boardAsArray = items.ToArray();

            if (boardAsArray.Take(insertIndex + 1)
                .All(x => x != EMPTY))
            {
                return (Array.Empty<char>(), 0);
            }

            boardAsArray[(column * 2) + 5 + moveIndex] = EMPTY;

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

                extractEnergyCost += StepValue(
                    currentValue, at is 1
                        ? 1
                        : 2
                );
                InsertAt(currentValue, at - 1);
                boardAsArray[at] = c;
            }
        }

        private static long StepValue(char c, int steps)
        {
            return (long)Math.Pow(x: 10, c - 65) * steps;
        }

        private static ReadOnlySpan<char> ColumnByNumber(ReadOnlySpan<char> items, int columnNr)
        {
            return items.Slice((columnNr * 2) + 5, length: 2);
        }

        private static bool EmptyOrContainsOnly(in ReadOnlySpan<char> column, char c)
        {
            return (column[index: 0] == c || column[index: 0] == EMPTY)
                && (column[index: 1] == c || column[index: 1] == EMPTY);
        }

        public static bool IsColumnFull(ReadOnlySpan<char> items)
        {
            return items[index: 0] != EMPTY && items[index: 1] != EMPTY;
        }

        public static bool IsColumnEmpty(ReadOnlySpan<char> items)
        {
            return items[index: 0] == EMPTY && items[index: 1] == EMPTY;
        }

        public static (char[] board, long score) MoveColumnToRight(ReadOnlySpan<char> items, int column)
        {
            var extractColumn = ColumnByNumber(items, column);

            if (extractColumn.IsEmpty)
            {
                return (Array.Empty<char>(), 0);
            }

            return MoveColumnToRight(items, extractColumn, column);
        }

        private static (char[] board, long score) MoveColumnToRight(ReadOnlySpan<char> items, ReadOnlySpan<char> extractColumn, int column)
        {
            var boardAsArray = items.ToArray();
            var extractValue = extractColumn[index: 0] == EMPTY
                ? extractColumn[index: 1]
                : extractColumn[index: 0];
            var extractEnergyCost = StepValue(
                extractValue, extractValue == extractColumn[index: 0]
                    ? 2
                    : 3
            );
            var insertIndex = column + 1;
            var areAllEmpty = boardAsArray
                .Take(count: 7)
                .Skip(insertIndex)
                .All(x => x != EMPTY);

            if (areAllEmpty)
            {
                return (Array.Empty<char>(), 0);
            }

            boardAsArray[(column * 2) + 5 + (extractValue == extractColumn[index: 0]
                ? 0
                : 1)] = EMPTY;

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

                extractEnergyCost += StepValue(
                    currentValue, at is 5
                        ? 1
                        : 2
                );
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
