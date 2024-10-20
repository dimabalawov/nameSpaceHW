using System;
using System.Collections.Generic;

namespace TicTacToe
{
    enum Player { None = 0, Human1, Human2, Computer }

    class Program
    {
        static char[] board = new char[9] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static Player currentPlayer;
        static Random random = new Random();
        static bool playingAgainstComputer;

        static void Main(string[] args)
        {
            Console.WriteLine("Choose an application: ");
            Console.WriteLine("1. Tic-Tac-Toe");
            Console.WriteLine("2. Text to Morse Code and Morse Code to Text Converter");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                StartTicTacToe();
            }
            else if (choice == 2)
            {
                MorseCodeConverter.Program.RunMorseCodeConverter();
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        static void StartTicTacToe()
        {
            Console.WriteLine("Welcome to Tic-Tac-Toe!");
            Console.WriteLine("Do you want to play against the computer? (yes/no)");
            playingAgainstComputer = Console.ReadLine().ToLower() == "yes";

            currentPlayer = random.Next(0, 2) == 0 ? Player.Human1 : (playingAgainstComputer ? Player.Computer : Player.Human2);

            PlayGame();
        }

        static void PlayGame()
        {
            while (true)
            {
                Console.Clear();
                DrawBoard();
                if (CheckWin())
                {
                    Console.WriteLine($"{currentPlayer} wins!");
                    break;
                }
                else if (IsBoardFull())
                {
                    Console.WriteLine("It's a draw!");
                    break;
                }

                if (currentPlayer == Player.Human1 || currentPlayer == Player.Human2)
                {
                    PlayerMove();
                }
                else
                {
                    ComputerMove();
                }

                currentPlayer = SwitchPlayer(currentPlayer);
            }
        }

        static void DrawBoard()
        {
            Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
        }

        static void PlayerMove()
        {
            int move;
            Console.WriteLine($"{currentPlayer}, enter your move (1-9): ");
            while (!int.TryParse(Console.ReadLine(), out move) || move < 1 || move > 9 || board[move - 1] == 'X' || board[move - 1] == 'O')
            {
                Console.WriteLine("Invalid move, try again.");
            }
            board[move - 1] = currentPlayer == Player.Human1 ? 'X' : 'O';
        }

        static void ComputerMove()
        {
            int move;
            do
            {
                move = random.Next(1, 10);
            } while (board[move - 1] == 'X' || board[move - 1] == 'O');

            Console.WriteLine("Computer chooses: " + move);
            board[move - 1] = 'O';
        }

        static Player SwitchPlayer(Player currentPlayer)
        {
            if (playingAgainstComputer)
            {
                return currentPlayer == Player.Human1 ? Player.Computer : Player.Human1;
            }
            else
            {
                return currentPlayer == Player.Human1 ? Player.Human2 : Player.Human1;
            }
        }

        static bool CheckWin()
        {
            int[,] winCombinations = new int[,]
            {
                { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, 
                { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 },
                { 0, 4, 8 }, { 2, 4, 6 }           
            };

            for (int i = 0; i < winCombinations.GetLength(0); i++)
            {
                if (board[winCombinations[i, 0]] == board[winCombinations[i, 1]] &&
                    board[winCombinations[i, 1]] == board[winCombinations[i, 2]])
                {
                    return true;
                }
            }
            return false;
        }

        static bool IsBoardFull()
        {
            foreach (var spot in board)
            {
                if (spot != 'X' && spot != 'O')
                {
                    return false;
                }
            }
            return true;
        }
    }
}

namespace MorseCodeConverter
{
    class Program
    {
        static Dictionary<char, string> textToMorse = new Dictionary<char, string>()
        {
            {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."}, {'F', "..-."},
            {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"}, {'K', "-.-"}, {'L', ".-.."},
            {'M', "--"}, {'N', "-."}, {'O', "---"}, {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."},
            {'S', "..."}, {'T', "-"}, {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"},
            {'Y', "-.--"}, {'Z', "--.."}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"},
            {'5', "....."}, {'6', "-...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."}, {'0', "-----"},
            {' ', " "}
        };

        static Dictionary<string, char> morseToText = new Dictionary<string, char>()
        {
            {".-", 'A'}, {"-...", 'B'}, {"-.-.", 'C'}, {"-..", 'D'}, {".", 'E'}, {"..-.", 'F'},
            {"--.", 'G'}, {"....", 'H'}, {"..", 'I'}, {".---", 'J'}, {"-.-", 'K'}, {".-..", 'L'},
            {"--", 'M'}, {"-.", 'N'}, {"---", 'O'}, {".--.", 'P'}, {"--.-", 'Q'}, {".-.", 'R'},
            {"...", 'S'}, {"-", 'T'}, {"..-", 'U'}, {"...-", 'V'}, {".--", 'W'}, {"-..-", 'X'},
            {"-.--", 'Y'}, {"--..", 'Z'}, {".----", '1'}, {"..---", '2'}, {"...--", '3'}, {"....-", '4'},
            {".....", '5'}, {"-....", '6'}, {"--...", '7'}, {"---..", '8'}, {"----.", '9'}, {"-----", '0'},
            {" ", ' '}
        };

        public static void RunMorseCodeConverter()
        {
            Console.WriteLine("Choose: ");
            Console.WriteLine("1. Convert Text to Morse Code");
            Console.WriteLine("2. Convert Morse Code to Text");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                ConvertTextToMorse();
            }
            else if (choice == 2)
            {
                ConvertMorseToText();
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        static void ConvertTextToMorse()
        {
            Console.WriteLine("Enter text to convert to Morse code:");
            string input = Console.ReadLine().ToUpper();
            string morseOutput = "";

            foreach (char c in input)
            {
                if (textToMorse.ContainsKey(c))
                {
                    morseOutput += textToMorse[c] + " ";
                }
                else
                {
                    morseOutput += "? "; 
                }
            }

            Console.WriteLine("Morse Code: " + morseOutput);
        }

        static void ConvertMorseToText()
        {
            Console.WriteLine("Enter Morse code to convert to text (separate symbols by spaces):");
            string input = Console.ReadLine();
            string[] morseSymbols = input.Split(' ');
            string textOutput = "";

            foreach (string symbol in morseSymbols)
            {
                if (morseToText.ContainsKey(symbol))
                {
                    textOutput += morseToText[symbol];
                }
                else
                {
                    textOutput += "?";
                }
            }

            Console.WriteLine("Text: " + textOutput);
        }
    }
}
