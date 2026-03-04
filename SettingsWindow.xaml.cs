using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public string Difficulty { get; private set; }
        public string Player { get; private set; }

        public SettingsWindow(string currentDifficulty, string currentPlayer)
        {
            InitializeComponent();

            // Set difficulty radio buttons
            if (currentDifficulty == "Easy")
                Easy.IsChecked = true;
            else if (currentDifficulty == "Medium")
                Medium.IsChecked = true;
            else if (currentDifficulty == "Hard")
                Hard.IsChecked = true;

            // Set first player radio buttons
            if (currentPlayer == "Xs")
                Xs.IsChecked = true;
            else if (currentPlayer == "Os")
                Os.IsChecked = true;
            else if (currentPlayer == "Alternate")
                Alternate.IsChecked = true;

            Difficulty = currentDifficulty;
            Player = currentPlayer;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            // Read radio button states
            if (Easy.IsChecked == true)
                Difficulty = "Easy";
            else if (Medium.IsChecked == true)
                Difficulty = "Medium";
            else if (Hard.IsChecked == true)
                Difficulty = "Hard";

            if (Xs.IsChecked == true)
                Player = "Xs";
            else if (Os.IsChecked == true)
                Player = "Os";
            else if (Alternate.IsChecked == true)
                Player = "Alternate";

            DialogResult = true;
        }
    }
}
