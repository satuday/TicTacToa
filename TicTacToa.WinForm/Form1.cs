using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TicTacToa.WinForm
{
    public partial class Form1 : Form
    {
        TicTacToa.Game game;
        TicTacToa.Board board;
        TicTacToa.Player player1;
        TicTacToa.Player player2;
        KeyValuePair<Player, Image> p1;
        KeyValuePair<Player, Image> p2;

        KeyValuePair<TicTacToa.Player, Image> currentPlayer;

        public Form1()
        {
            InitializeComponent();

            player1 = new Player("Player1");
            player2 = new Player("Player2");
            board = new Board(player1, player2);
            game = new Game(board, player1, player2);
            newGame();
            foreach (PictureBox pb in flowLayoutPanel1.Controls)
            {
                pb.Click += pictureBox_Click;
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb.Image == null)
            {
                pb.Image = currentPlayer.Value;
                currentPlayer.Key.NextMove(Convert.ToInt16(pb.Tag));
                Player winner;
                if(board.HasWinner(out  winner))
                {
                    MessageBox.Show("winner is " + winner.Name);
                }
                currentPlayer = currentPlayer.Equals(p1) ? p2:p1;            

            }
            //pb.Image = global::TicTacToa.WinForm.Properties.Resources.
        }

        void newGame()
        {

            p1 = new KeyValuePair<Player, Image>(player1, Properties.Resources.x);
            p2 = new KeyValuePair<Player, Image>(player2, Properties.Resources.o);
            currentPlayer = p1;
            foreach (PictureBox pb in flowLayoutPanel1.Controls)
            {
                pb.Image = null;
            }
            game.NewGame();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newGame();

        }
    }
}
