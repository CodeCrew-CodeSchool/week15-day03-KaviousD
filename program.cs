using System;

namespace tictactoe
{
    class Player
    {
        public string id;
        public string marker;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to KD's Anime Tic Tac Toe game!\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            Console.Clear();

            Console.WriteLine("You are Goku. You will be using X\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            Console.Clear();

            string[][] gameBoard = new string[][]
            {
                new string[] { "1", "2", "3" },
                new string[] { "4", "5", "6" },
                new string[] { "7", "8", "9" }
            };

            Player player1 = new Player() { id = "1", marker = "X" };
            Player player2 = new Player() { id = "2", marker = "O" };

            Player currentPlayer = player1;
            bool gameOver = false;
            while (!gameOver)
            {
                Console.WriteLine("This is the board\n");
                DisplayBoard(gameBoard);

                Console.WriteLine($"\nPlayer {currentPlayer.id}. Make a move!");
                string playerChoice = Console.ReadLine();

                try
                {
                    MarkBoard(playerChoice, currentPlayer, gameBoard);
                    Console.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please choose again.");
                    continue; // Restarts da loop if two numbers are chosen
                }

                // condition for tie/wins
                if (CheckForWin(gameBoard, currentPlayer.marker))
                {
                    Console.WriteLine($"Player {currentPlayer.id} wins!");
                    gameOver = true;
                }
                else if (CheckForTie(gameBoard))
                {
                    Console.WriteLine("It's a tie!");
                    gameOver = true;
                }
                else
                {
                    // players turn advocator
                    currentPlayer = (currentPlayer == player1) ? player2 : player1;
                }
            }
        }

        static void DisplayBoard(string[][] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                string[] row = board[i];
                for (int x = 0; x < row.Length; x++)
                {
                    string value = row[x];
                    Console.Write("|" + value + "|");
                }
                Console.WriteLine();
            }
        }

        static void MarkBoard(string playerChoice, Player currentPlayer, string[][] gameBoard)
        {
            int choice;
            if (!int.TryParse(playerChoice, out choice) || choice < 1 || choice > 9)
                throw new Exception("Invalid input. Please enter a number between 1 and 9.");

            int row = (choice - 1) / 3;
            int col = (choice - 1) % 3;

            if (gameBoard[row][col] == "X" || gameBoard[row][col] == "O")
                throw new Exception("Spot is already taken");

            gameBoard[row][col] = currentPlayer.marker;
        }

        static bool CheckForWin(string[][] board, string marker)
        {
            // Check  da rows for loot
            for (int i = 0; i < 3; i++)
            {
                if (board[i][0] == marker && board[i][1] == marker && board[i][2] == marker)
                    return true;
            }

            // Check da columns for loot
            for (int i = 0; i < 3; i++)
            {
                if (board[0][i] == marker && board[1][i] == marker && board[2][i] == marker)
                    return true;
            }

            // Check da diagonal lines for loot
            if ((board[0][0] == marker && board[1][1] == marker && board[2][2] == marker) ||
                (board[0][2] == marker && board[1][1] == marker && board[2][0] == marker))
                return true;

            return false;
        }

        static bool CheckForTie(string[][] board)
        {
            foreach (string[] row in board)
            {
                foreach (string cell in row)
                {
                    if (cell != "X" && cell != "O")
                        return false;
                }
            }
            return true;
        }
    }
}
