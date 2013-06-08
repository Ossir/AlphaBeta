using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaBeta
{
    class Program
    {
        static int Computer = 0, Player = 1;
        static int GameOver = 100;
        static int MinNumber = 1, MaxNumber = 10;
        static int StepDeep = 10;
        static int turn = Computer;

        static int Cut(int s, int step, bool max, int alpha, int beta)
        {
            List<int> LookahedTree = new List<int>();
            int value;
            int result = 0;
            if (s < GameOver)
            {
                if (step < StepDeep)
                {
                    for (int i = MaxNumber; i >= MinNumber; i--)
                    {
                        value = Cut(s + i, step + 1, !max, alpha, beta);
                        LookahedTree.Add(value);
                        if (max && value > alpha)
                        {
                            alpha = value;
                        }
                        else if (!max && value < beta)
                        {
                            beta = value;
                        }
                        if (alpha >= beta)
                        {
                            break;
                        }
                    }
                    if (step > 0)
                    {
                        if (max)
                        {
                            result = LookahedTree.Max();
                        }
                        else
                        {
                            result = LookahedTree.Min();
                        }
                    }
                    else
                    {
                        int bestValue = 0;
                        int bestNumber = 1;
                        for (int i = 0; i < LookahedTree.Count; i++)
                        {
                            if (bestValue < LookahedTree[i])
                            {
                                bestValue = LookahedTree[i];
                                bestNumber = MaxNumber - i;
                            }
                        }
                        result = bestNumber;
                    }
                }
                else
                {
                    result = GameOver - step;
                }
            }
            else
            {
                if (max)
                {
                    result = GameOver - step;
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }

        static int DoStep(int s)
        {
            int result = Cut(s, 0, true, Int32.MinValue, Int32.MaxValue);
            return result;
        }

        static void ChangeTurn()
        {
            if (turn == Computer)
                turn = Player;
            else
                turn = Computer;
        }

        static void Main(string[] args)
        {
            int sum = 0, step;
            while (sum < GameOver)
            {
                if (turn == Computer)
                {
                    step = DoStep(sum);
                    Console.WriteLine("my step is " + step);
                }
                else
                {
                    do
                    {
                        step = Convert.ToInt32(Console.ReadLine());
                        if (step < MinNumber || step > MaxNumber)
                        {
                            Console.WriteLine("Number must be in range of 1 to 10");
                        }
                    }
                    while (step < MinNumber || step > MaxNumber);
                }
                sum += step;
                Console.WriteLine("Current sum = " + sum);
                if (sum < GameOver)
                {
                    ChangeTurn();
                }
                else
                {
                    ChangeTurn();
                    Console.WriteLine("Game over. Winner is " + turn);
                    Console.ReadLine();
                }
            }
        }
    }
}
