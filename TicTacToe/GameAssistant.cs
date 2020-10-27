using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class GameAssistant : Form
    {
        private List<Player> players = new List<Player>();
        private int _currentPlayer = 0;
        private Playground playground;
        private List<Button> fields;
        private bool activeGame;
        private Random r = new Random();

        private List<List<int>> _allVariations = new List<List<int>>()
        {
          new List<int>(){0,1,2},
          new List<int>(){3,4,5},
          new List<int>(){6,7,8},
          new List<int>(){0,3,6},
          new List<int>(){1,4,7},
          new List<int>(){2,5,8},
          new List<int>(){0,4,8},
          new List<int>(){2,4,6}
        };

        private List<Button> _allFields = new List<Button>();

        public GameAssistant()
        {
            InitializeComponent();
            players.Add(new Player("X"));
            players.Add(new Player("O"));
            foreach (var button in this.Controls.OfType<Button>())
                _allFields.Add(button);
            playground = new Playground(_allFields);
            StartNewGame();
        }

        public void ChangePlayer()
        {
            if (_currentPlayer == 0)
                _currentPlayer = 1;
            else
                _currentPlayer = 0;
            lbCurrentPlayer.Text = "Spieler " + players[_currentPlayer].GetName() + " ist an der Reihe";
        }

        public void ShowWinner(string player)
        {
            lbWinnerState.Text = player + " hat gewonnen. Neues Spiel starten?";
            activeGame = false;
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text != "" || !activeGame)
                return;

            if (_currentPlayer == 0)
                button.Text = players[_currentPlayer].GetName();
            else
                button.Text = players[_currentPlayer].GetName();

            GameResult playerHasWon = CheckState(_currentPlayer);

            if (playerHasWon == GameResult.Won)
            {
                ShowWinner(players[_currentPlayer].GetName());
            }
            else if (playerHasWon == GameResult.Tie)
                ShowWinner("Niemand");
            else
                ChangePlayer();
        }

        private enum GameResult
        {
            Won,
            Tie,
            Ingame
        }

        private GameResult CheckState(int currentPlayer)
        {
            fields = playground.GetFields();

            foreach (var line in _allVariations)
            {
                bool win = CheckLine(line);

                if (win)
                    return GameResult.Won;

                int count = 0;
                foreach (var field in fields)
                    if (field.Text != String.Empty)
                        count++;

                if (count == 9)
                    return GameResult.Tie;
            }
            return GameResult.Ingame;
        }

        private bool CheckLine(List<int> line)
        {
            string name = players[_currentPlayer].GetName();
            if (fields[line[0]].Text == name && fields[line[1]].Text == name && fields[line[2]].Text == name)
                return true;
            return false;
        }

        private void lbWinnerState_Click(object sender, EventArgs e)
        {
            foreach (var field in _allFields)
                field.Text = String.Empty;
            lbWinnerState.Text = String.Empty;
            StartNewGame();
        }

        private void StartNewGame()
        {
            _currentPlayer = r.Next(2);
            activeGame = true;
            lbCurrentPlayer.Text = "Spieler " + players[_currentPlayer].GetName() + " beginnt.";
        }
    }

    public class NoFocusCueButton : Button
    {
        public NoFocusCueButton() : base()
        {
        }

        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }
    }
}