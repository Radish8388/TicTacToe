using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class PlayHardAI
    {
        public PlayHardAI() { }
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

            int move = -1;
            int score = -2;

            for (int i = 0; i < 9; ++i)
            {

                if (board[i] == 0)
                {
                    board[i] = 1;
                    int tempScore = -minimax(board, -1);
                    board[i] = 0;

                    if (tempScore > score)
                    {
                        score = tempScore;
                        move = i;
                    }
                }
            }
            // returns a score based on minimax tree at a given node.
            return move;
        }

        private int minimax(int[] board, int player)
        {
            int winner = win(board);

            if (winner != 0)
                return winner * player;

            int move = -1;
            int score = -2;

            for (int i = 0; i < 9; i++)
            {

                if (board[i] == 0)
                {

                    board[i] = player;
                    int thisScore = -minimax(board, player * -1);

                    if (thisScore > score)
                    {
                        score = thisScore;
                        move = i;
                    }
                    board[i] = 0; // Reset board after try
                }
            }
            if (move == -1)
                return 0;

            return score;
        }

        private int win(int[] board)
        {
            // determines if a player has won, returns 0 otherwise.
            int[,] wins = new int[8, 3]
            {
                {0,1,2}, {3,4,5}, {6,7,8},
                {0,3,6}, {1,4,7}, {2,5,8},
                {0,4,8}, {2,4,6}
            };

            int i;
            for (i = 0; i< 8; ++i) {
                if (board[wins[i,0]] != 0 &&
                    board[wins[i,0]] == board[wins[i,1]] &&
                    board[wins[i,1]] == board[wins[i,2]])
                    return board[wins[i,2]];
            }
            return 0;
        }

    }
}
