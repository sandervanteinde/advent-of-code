using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandervanteinde.AdventOfCode.Solutions._2022;

public class Day02 : BaseSolution
{
    public Day02()
        : base("Rock Paper Scissors", 2022, 2)
    {

    }
    public override object DetermineStepOneResult(FileReader reader)
    {
        const int WIN = 6;
        const int DRAW = 3;
        const int LOSS = 0;

        const int ROCK = 1;
        const int PAPER = 2;
        const int SCISSORS = 3;


        // A X Rock -- 1
        // B Y Paper -- 2
        // C Z Scisssors -- 3
        var score = 0;
        foreach(var line in reader.ReadLineByLine())
        {
            score += line switch
            {
                "A X" => ROCK + DRAW,
                "A Y" => PAPER + WIN,
                "A Z" => SCISSORS + LOSS,
                "B X" => ROCK + LOSS,
                "B Y" => PAPER + DRAW,
                "B Z" => SCISSORS + WIN,
                "C X" => ROCK + WIN,
                "C Y" => PAPER + LOSS,
                "C Z" => SCISSORS + DRAW 
            };
        }
        return score;
    }

    public override object DetermineStepTwoResult(FileReader reader)
    {
        const int WIN = 6;
        const int DRAW = 3;
        const int LOSS = 0;

        const int ROCK = 1;
        const int PAPER = 2;
        const int SCISSORS = 3;


        // A Rock -- 1
        // B Paper -- 2
        // C Scisssors -- 3
        // X Lose
        // Y Draw
        // Z Win
        var score = 0;
        foreach (var line in reader.ReadLineByLine())
        {
            score += line switch
            {
                "A X" => LOSS + SCISSORS,
                "A Y" => DRAW + ROCK ,
                "A Z" => WIN + PAPER,
                "B X" => LOSS + ROCK,
                "B Y" => DRAW + PAPER,
                "B Z" => WIN + SCISSORS,
                "C X" => LOSS + PAPER,
                "C Y" => DRAW + SCISSORS,
                "C Z" =>  WIN + ROCK
            };
        }
        return score;
    }
}
