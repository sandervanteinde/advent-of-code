using System.Text.RegularExpressions;

namespace Sandervanteinde.AdventOfCode.Solutions._2016;

public class Day10 : BaseSolution
{
    public Day10()
        : base("Balance Bots", year: 2016, day: 10)
    {
    }

    public override object DetermineStepOneResult(FileReader reader)
    {
        var botsById = new Dictionary<int, Bot>();
        var result = 0;
        reader.ReadAndProcessLineByLine(
            (line, source) =>
            {
                var goesToInstruction = Regex.Match(line, @"value (\d+) goes to bot (\d+)");

                if (goesToInstruction?.Success is true)
                {
                    var value = int.Parse(goesToInstruction.Groups[groupnum: 1].Value);
                    var botId = int.Parse(goesToInstruction.Groups[groupnum: 2].Value);
                    var bot = GetBotById(botId);
                    bot.AddChip(value);
                    return true;
                }

                var botInstruction = Regex.Match(line, @"bot (\d+) gives low to (bot|output) (\d+) and high to (bot|output) (\d+)");

                if (botInstruction is { Success: true })
                {
                    var botId = int.Parse(botInstruction.Groups[groupnum: 1].Value);
                    var bot = GetBotById(botId);

                    if (bot.ChipCount < 2)
                    {
                        return false;
                    }

                    if (bot.HasStepOneResult())
                    {
                        source.Cancel();
                        result = botId;
                        return true;
                    }

                    var lowOutputType = botInstruction.Groups[groupnum: 2].Value;
                    var lowOutputId = int.Parse(botInstruction.Groups[groupnum: 3].Value);
                    var highOutputType = botInstruction.Groups[groupnum: 4].Value;
                    var highOutputId = int.Parse(botInstruction.Groups[groupnum: 5].Value);

                    if (lowOutputType == "bot")
                    {
                        var lowBot = GetBotById(lowOutputId);
                        lowBot.AddChip(bot.RemoveAndGetLow());
                    }
                    else
                    {
                        bot.RemoveAndGetLow();
                    }

                    if (highOutputType == "bot")
                    {
                        var highBot = GetBotById(highOutputId);
                        highBot.AddChip(bot.RemoveAndGetHigh());
                    }
                    else
                    {
                        bot.RemoveAndGetHigh();
                    }
                }

                return false;

                Bot GetBotById(int botId)
                {
                    if (!botsById.TryGetValue(botId, out var bot))
                    {
                        botsById[botId] = bot = new Bot();
                    }

                    return bot;
                }
            }
        );

        return result;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        var botsById = new Dictionary<int, Bot>();
        var result = 0;
        var outputs = new Dictionary<int, int>();
        reader.ReadAndProcessLineByLine(
            (line, source) =>
            {
                var goesToInstruction = Regex.Match(line, @"value (\d+) goes to bot (\d+)");

                if (goesToInstruction?.Success is true)
                {
                    var value = int.Parse(goesToInstruction.Groups[groupnum: 1].Value);
                    var botId = int.Parse(goesToInstruction.Groups[groupnum: 2].Value);
                    var bot = GetBotById(botId);
                    bot.AddChip(value);
                    return true;
                }

                var botInstruction = Regex.Match(line, @"bot (\d+) gives low to (bot|output) (\d+) and high to (bot|output) (\d+)");

                if (botInstruction is { Success: true })
                {
                    var botId = int.Parse(botInstruction.Groups[groupnum: 1].Value);
                    var bot = GetBotById(botId);

                    if (bot.ChipCount < 2)
                    {
                        return false;
                    }

                    var lowOutputType = botInstruction.Groups[groupnum: 2].Value;
                    var lowOutputId = int.Parse(botInstruction.Groups[groupnum: 3].Value);
                    var highOutputType = botInstruction.Groups[groupnum: 4].Value;
                    var highOutputId = int.Parse(botInstruction.Groups[groupnum: 5].Value);

                    if (lowOutputType == "bot")
                    {
                        var lowBot = GetBotById(lowOutputId);
                        lowBot.AddChip(bot.RemoveAndGetLow());
                    }
                    else
                    {
                        outputs[lowOutputId] = bot.RemoveAndGetLow();
                    }

                    if (highOutputType == "bot")
                    {
                        var highBot = GetBotById(highOutputId);
                        highBot.AddChip(bot.RemoveAndGetHigh());
                    }
                    else
                    {
                        outputs[highOutputId] = bot.RemoveAndGetHigh();
                    }

                    if (outputs.ContainsKey(key: 0) && outputs.ContainsKey(key: 1) && outputs.ContainsKey(key: 2))
                    {
                        source.Cancel();
                        result = outputs[key: 0] * outputs[key: 1] * outputs[key: 2];
                    }
                }

                return false;

                Bot GetBotById(int botId)
                {
                    if (!botsById.TryGetValue(botId, out var bot))
                    {
                        botsById[botId] = bot = new Bot();
                    }

                    return bot;
                }
            }
        );

        return result;
    }

    public class Bot
    {
        public LinkedList<int> _chips = new();

        public int ChipCount => _chips.Count;

        public void AddChip(int chip)
        {
            var node = _chips.First;

            while (node is not null)
            {
                if (node.Value > chip)
                {
                    _chips.AddBefore(node, chip);
                    return;
                }

                node = node.Next;
            }

            _chips.AddLast(chip);
        }

        public bool HasStepOneResult()
        {
            return _chips is
            {
                First.Value: 17,
                Last.Value: 61
            };
        }

        public int RemoveAndGetLow()
        {
            var value = _chips.First!.Value;
            _chips.RemoveFirst();
            return value;
        }

        public int RemoveAndGetHigh()
        {
            var value = _chips.Last!.Value;
            _chips.RemoveLast();
            return value;
        }
    }
}
