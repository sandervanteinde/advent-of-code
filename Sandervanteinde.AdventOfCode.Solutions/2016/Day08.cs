using System.Buffers;
using System.Text;
using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2016;

internal partial class Day08 : BaseSolution
{
    private readonly Screen _screen;

    public Day08()
        : base("Two-Factor Authentication", 2016, 8)
    {
        _screen = new Screen(50, 6);
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        ProcessInstructions(reader);

        return _screen.TotalPixelsOn;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        ProcessInstructions(reader);
        return _screen.Print();
    }

    private void ProcessInstructions(FileReader reader)
    {
        foreach (var instruction in reader.ReadLineByLine())
        {
            var rectInstruction = Regex.Match(instruction, @"rect (\d+)x(\d+)");
            if (rectInstruction?.Success is true)
            {
                _screen.CreateRectangle(int.Parse(rectInstruction.Groups[1].Value), int.Parse(rectInstruction.Groups[2].Value));
                continue;
            }
            var rotateRowInstruction = Regex.Match(instruction, @"rotate row y=(\d+) by (\d+)");
            if (rotateRowInstruction?.Success is true)
            {
                _screen.RotateRow(int.Parse(rotateRowInstruction.Groups[1].Value), int.Parse(rotateRowInstruction.Groups[2].Value));
                continue;
            }

            var rotateColumnInstruction = Regex.Match(instruction, @"rotate column x=(\d+) by (\d+)");
            if (rotateColumnInstruction?.Success is true)
            {
                _screen.RotateColumn(int.Parse(rotateColumnInstruction.Groups[1].Value), int.Parse(rotateColumnInstruction.Groups[2].Value));
                continue;
            }

            throw new NotImplementedException();
        }
    }

    public class Screen
    {
        public int Width { get; }
        public int Height { get; }

        private readonly bool[,] _pixels;
        public int TotalPixelsOn { get; private set; }

        public Screen(int width, int height)
        {
            Width = width;
            Height = height;

            _pixels = new bool[width, height];
        }

        public void CreateRectangle(int width, int height)
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (!_pixels[i, j])
                    {
                        _pixels[i, j] = true;
                        TotalPixelsOn++;
                    }
                }
            }
        }

        public void RotateColumn(int column, int amount)
        {
            var columnValues = ArrayPool<bool>.Shared.Rent(Height);
            for (var i = 0; i < Height; i++)
            {
                columnValues[i] = _pixels[column, i];
            }
            for (var i = 0; i < Height; i++)
            {
                _pixels[column, (i + amount) % Height] = columnValues[i];
            }
            ArrayPool<bool>.Shared.Return(columnValues);
        }

        public void RotateRow(int row, int amount)
        {
            var rowValues = ArrayPool<bool>.Shared.Rent(Width);
            for (var i = 0; i < Width; i++)
            {
                rowValues[i] = _pixels[i, row];
            }
            for (var i = 0; i < Width; i++)
            {
                _pixels[(i + amount) % Width, row] = rowValues[i];
            }
            ArrayPool<bool>.Shared.Return(rowValues);
        }

        public string Print()
        {
            var sb = new StringBuilder();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    sb.Append(_pixels[x, y] ? '#' : '.');
                }

                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
