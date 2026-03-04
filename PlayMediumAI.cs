using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TicTacToe
{
    internal class PlayMediumAI
    {
        private Random random = new Random();
        public PlayMediumAI() { }
        public int GetPlay(int[] board)
        {
            // board indexes:
            // ----++---++----
            // | 0 || 1 || 2 |
            // ----++---++----
            // | 3 || 4 || 5 |
            // ----++---++----
            // | 6 || 7 || 8 |
            // ----++---++----
            // board values:
            // Player = -1 ; Computer = 1 ; blank square = 0
            // return value:
            // board index to make next play

            // Try each strategy in order
            int square;
            if (TryToWin(board, out square)) return square;
            if (TryToBlock(board, out square)) return square;
            if (TryToMakeTwoInRow(board, out square)) return square;
            if (StrategicSquare(board, out square)) return square;
            return GetRandomEmptySquare(board);

        }

        private bool StrategicSquare(int[] board, out int square)
        {
            if (board[4] == 0) 
            {
                square = 4;
                return true;
            }

            List<int> plays = new List<int>();
            if (board[0] == 0) plays.Add(0);
            if (board[2] == 0) plays.Add(2);
            if (board[6] == 0) plays.Add(6);
            if (board[8] == 0) plays.Add(8);

            if (plays.Count > 0)
            {
                square = plays[random.Next(plays.Count)];
                return true;
            }

            square = 0;
            return false;
        }

        private int GetRandomEmptySquare(int[] board)
        {
            int play = -1;
            List<int> emptySquares = new List<int>();
            for (int s = 0; s < board.Length; s++)
                if (board[s] == 0)
                    emptySquares.Add(s);
            if (emptySquares.Count > 0)
                play = emptySquares[random.Next(emptySquares.Count)];
            return play;
        }

        private bool TryToMakeTwoInRow(int[] board, out int square)
        {
            int[,] wins = new int[8, 3]
            {
                {0,1,2}, {3,4,5}, {6,7,8}, // rows
                {0,3,6}, {1,4,7}, {2,5,8}, // columns
                {0,4,8}, {2,4,6}           // diagonals
            };
            List<int> plays = new List<int>();

            for (int i = 0; i < 8; ++i)
            {
                if ((board[wins[i, 0]] == 1) && (board[wins[i, 1]] == 0) && (board[wins[i, 2]] == 0))
                {
                    plays.Add(wins[i, 1]);
                    plays.Add(wins[i, 2]);
                }
                else if ((board[wins[i, 0]] == 0) && (board[wins[i, 1]] == 1) && (board[wins[i, 2]] == 0))
                {
                    plays.Add(wins[i, 0]);
                    plays.Add(wins[i, 2]);
                }
                else if ((board[wins[i, 0]] == 0) && (board[wins[i, 1]] == 0) && (board[wins[i, 2]] == 1))
                {
                    plays.Add(wins[i, 0]);
                    plays.Add(wins[i, 1]);
                }
            }

            Debug.Write("2inarow plays = ");
            for (int i = 0; i < plays.Count; ++i)
                Debug.Write($"{plays[i]}, ");
            Debug.WriteLine("");

            if (plays.Count > 0)
            {
                square = plays[random.Next(plays.Count)];
                return true;
            }

            square = 0;
            return false;
        }

        private bool TryToBlock(int[] board, out int square)
        {
            int[,] wins = new int[8, 3]
            {
                {0,1,2}, {3,4,5}, {6,7,8}, // rows
                {0,3,6}, {1,4,7}, {2,5,8}, // columns
                {0,4,8}, {2,4,6}           // diagonals
            };
            List<int> plays = new List<int>();

            for (int i = 0; i < 8; ++i)
            {
                if ((board[wins[i, 0]] == -1) && (board[wins[i, 1]] == -1) && (board[wins[i, 2]] == 0))
                    plays.Add(wins[i, 2]);
                else if ((board[wins[i, 0]] == -1) && (board[wins[i, 1]] == 0) && (board[wins[i, 2]] == -1))
                    plays.Add(wins[i, 1]);
                else if ((board[wins[i, 0]] == 0) && (board[wins[i, 1]] == -1) && (board[wins[i, 2]] == -1))
                    plays.Add(wins[i, 0]);
            }

            Debug.Write("block plays = ");
            for (int i = 0; i < plays.Count; ++i)
                Debug.Write($"{plays[i]}, ");
            Debug.WriteLine("");

            if (plays.Count > 0)
            {
                square = plays[random.Next(plays.Count)];
                return true;
            }

            square = 0;
            return false;
        }

        private bool TryToWin(int[] board, out int square)
        {
            int[,] wins = new int[8, 3]
            {
                {0,1,2}, {3,4,5}, {6,7,8}, // rows
                {0,3,6}, {1,4,7}, {2,5,8}, // columns
                {0,4,8}, {2,4,6}           // diagonals
            };
            List<int> plays = new List<int>();

            for (int i = 0; i < 8; ++i)
            {
                if ((board[wins[i, 0]] == 1) && (board[wins[i, 1]] == 1) && (board[wins[i, 2]] == 0))
                    plays.Add(wins[i, 2]);
                else if ((board[wins[i, 0]] == 1) && (board[wins[i, 1]] == 0) && (board[wins[i, 2]] == 1))
                    plays.Add(wins[i, 1]);
                else if ((board[wins[i, 0]] == 0) && (board[wins[i, 1]] == 1) && (board[wins[i, 2]] == 1))
                    plays.Add(wins[i, 0]);
            }

            Debug.Write("win plays = ");
            for (int i = 0; i < plays.Count; ++i)
                Debug.Write($"{plays[i]}, ");
            Debug.WriteLine("");

            if (plays.Count > 0)
            {
                square = plays[random.Next(plays.Count)];
                return true;
            }

            square = 0;
            return false;
        }
    }
}
