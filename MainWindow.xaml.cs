// must import NuGet package System.Speech

using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SpeechSynthesizer synth;
        // Store current settings
        private string difficulty = "Easy";  // defaults
        private string player = "Xs";
        private GameState game = new GameState();
        private int numberOfWins = 0;
        private int numberOfLosses = 0;
        private int numberOfDraws = 0;
        private string playerToken = "X";
        private string computerToken = "O";

        public MainWindow()
        {
            InitializeComponent();
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            // Set the volume (0-100)
            synth.Volume = 75;
            // Set the speed (-10 to 10)
            synth.Rate = -1;
            synth.SelectVoiceByHints(VoiceGender.Female);
        }

        private void Square_Clicked(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.ToString() != " " || game.IsGameComplete())
                return;

            if (!int.TryParse(button.Tag.ToString(), out int square))
                return;
            game.Play(square);

            button.Content = playerToken;
            if (game.IsGameComplete())
                ShowWinner();
            else
                ComputerPlay();
        }

        private void ComputerPlay()
        {
            int square = -1;

            if (difficulty == "Easy")
                square = game.GetComputerPlay(0);
            else if (difficulty == "Medium")
                square = game.GetComputerPlay(1);
            else if (difficulty == "Hard")
                square = game.GetComputerPlay(2);

            switch (square)
            {
                case 0: Square0.Content = computerToken; break;
                case 1: Square1.Content = computerToken; break;
                case 2: Square2.Content = computerToken; break;
                case 3: Square3.Content = computerToken; break;
                case 4: Square4.Content = computerToken; break;
                case 5: Square5.Content = computerToken; break;
                case 6: Square6.Content = computerToken; break;
                case 7: Square7.Content = computerToken; break;
                case 8: Square8.Content = computerToken; break;
            }

            if (game.IsGameComplete())
                ShowWinner();
        }

        private void ShowWinner()
        {
            int winner = game.WhoWon();
            if (winner == -1)
            {
                SpeakText("you won");
                numberOfWins++;
            }
            else if (winner == 1)
            {
                SpeakText("I won");
                numberOfLosses++;
            }
            else
            {
                SpeakText("it's a draw");
                numberOfDraws++;
            }
            RecordText.Text = $"Record: {numberOfWins}-{numberOfLosses}-{numberOfDraws}";
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void NewGame()
        {
            foreach (Button button in mainGrid.Children.OfType<Button>())
            {
                if (button != NewGameButton && button != SettingsButton)
                    button.Content = " ";
            }
            game.NewGame();
            if (player == "Alternate")
            {
                if (playerToken == "X")
                {
                    playerToken = "O";
                    computerToken = "X";
                }
                else
                {
                    playerToken = "X";
                    computerToken = "O";
                }
            }
            PlayerText.Text = "You are playing " + playerToken + "s";
            if (playerToken == "O")  ComputerPlay();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(difficulty, player);
            settingsWindow.Owner = this;
            bool? result = settingsWindow.ShowDialog(); // Modal dialog

            if (result == true)  // User clicked OK
            {
                // Save the settings
                difficulty = settingsWindow.Difficulty;
                player = settingsWindow.Player;

                string previousToken = playerToken;
                if (player == "Xs")
                {
                    playerToken = "X";
                    computerToken = "O";
                }
                if (player == "Os")
                {
                    playerToken = "O";
                    computerToken = "X";
                }
                if (previousToken != playerToken) NewGame();
            }

        }

        public void SpeakText(string text)
        {
            // Speak synchronously (freezes UI) or asynchronously (better for WPF)
            synth.SpeakAsync(text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // load the properties from disk
            Properties.Settings.Default.Reload();

            // check for upgrade
            if (Properties.Settings.Default.UpgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeRequired = false;
                Properties.Settings.Default.Save();
            }

            this.Left = Properties.Settings.Default.WindowLeft;
            this.Top = Properties.Settings.Default.WindowTop;
            this.Width = Properties.Settings.Default.WindowWidth;
            this.Height = Properties.Settings.Default.WindowHeight;

            double screenWidth = SystemParameters.WorkArea.Width;
            double screenHeight = SystemParameters.WorkArea.Height;

            // ensure window size doesn't exceed screen size
            if (this.Width > screenWidth) this.Width = screenWidth;
            if (this.Height > screenHeight) this.Height = screenHeight;

            // ensure window is not off the left or top
            if (this.Left < 0) this.Left = 0;
            if (this.Top < 0) this.Top = 0;

            // ensure window is not off the right or bottom
            if (this.Left + this.Width > screenWidth)
                this.Left = screenWidth - this.Width;
            if (this.Top + this.Height > screenHeight)
                this.Top = screenHeight - this.Height;

            if (Properties.Settings.Default.WindowState == "Maximized")
                this.WindowState = WindowState.Maximized;

            difficulty = Properties.Settings.Default.Difficulty;
            player = Properties.Settings.Default.Player;
            if (player == "Os")
            {
                playerToken = "O";
                computerToken = "X";
            }
            else
            {
                playerToken = "X";
                computerToken = "O";
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.WindowState = this.WindowState.ToString();
            if (this.WindowState == WindowState.Normal)
            {
                Properties.Settings.Default.WindowLeft = this.Left;
                Properties.Settings.Default.WindowTop = this.Top;
                Properties.Settings.Default.WindowWidth = this.Width;
                Properties.Settings.Default.WindowHeight = this.Height;
            }

            // save other properties here
            Properties.Settings.Default.Difficulty = difficulty;
            Properties.Settings.Default.Player = player;

            Properties.Settings.Default.Save();
        }
    }
}
