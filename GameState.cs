using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TicTacToe
{
    internal class GameState
    {
        private int[] board;
        private Random random = new Random();
        private PlayMediumAI playMedium = new PlayMediumAI();
        private PlayHardAI playHard = new PlayHardAI();

        public GameState() {
            board = new int[9];
            NewGame();
        }

        public void NewGame()
        {
            for (int i=0; i<board.Length; i++)
                board[i] = 0;
        }

        public void Play(int square)
        {
            if (square >= 0 && square < board.Length)
                board[square] = -1;
        }

        public bool IsGameComplete()
        {
            bool areEmptySquaresRemaining = false;
            for (int s = 0; s < board.Length; s++)
                if (board[s] == 0)
                    areEmptySquaresRemaining = true;
            if (areEmptySquaresRemaining == false)
                return true;

            int[,] wins = new int[8, 3]
            {
                {0,1,2}, {3,4,5}, {6,7,8},
                {0,3,6}, {1,4,7}, {2,5,8},
                {0,4,8}, {2,4,6}
            };
            for (int i = 0; i < 8; ++i)
            {
                if (board[wins[i,0]] != 0 &&
                    board[wins[i,0]] == board[wins[i,1]] && board[wins[i,1]] == board[wins[i,2]])
                    return true;
            }
            return false;
        }

        public int GetComputerPlay(int difficulty)
        {
            int play = -1;
            switch (difficulty)
            {
                case 0: play = PlayEasy(); break;
                case 1: play = PlayMedium(); break;
                case 2: play = PlayHard(); break;
            }
            if (play >= 0 && play < board.Length)
                board[play] = 1;
            return play;
        }

        private int PlayHard()
        {
            int play = playHard.GetPlay(board);
            //Debug.WriteLine($"Play Hard's play: {play}");
            return play;
        }

        private int PlayMedium()
        {
            int play = playMedium.GetPlay(board);
            Debug.WriteLine($"Play Medium's play: {play}");
            return play;
        }

        private int PlayEasy()
        {
            int play = -1;
            List<int> emptySquares = new List<int>();
            for (int s=0; s<board.Length; s++)
                if (board[s] == 0)
                    emptySquares.Add(s);
            if (emptySquares.Count > 0)
                play = emptySquares[random.Next(emptySquares.Count)];
            //Debug.WriteLine($"Play Easy's play = {play}");
            return play;
        }

        public int WhoWon()
        {
            int[,] wins = new int[8, 3]
            {
                {0,1,2}, {3,4,5}, {6,7,8},
                {0,3,6}, {1,4,7}, {2,5,8},
                {0,4,8}, {2,4,6}
            };

            for (int i = 0; i < 8; ++i)
            {
                if (board[wins[i, 0]] != 0 &&
                    board[wins[i, 0]] == board[wins[i, 1]] && board[wins[i, 1]] == board[wins[i, 2]])
                    return board[wins[i, 0]];
            }

            return 0; 
        }
    }
}
