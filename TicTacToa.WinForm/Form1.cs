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
        TicTacToa.Player player1;
        TicTacToa.Player player2;
        KeyValuePair<Player, Image> p1;
        KeyValuePair<Player, Image> p2;

        bool isXturn = true;

        TicTacToaBoard tboard;

        public Form1()
        {
            InitializeComponent();

            tboard = new TicTacToaBoard();

            player1 = new Player("Player1");
            player2 = new Player("Player2");
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
                if (isXturn)
                {
                    pb.Image = p1.Value;
                    tboard[Convert.ToInt16(pb.Tag)-1] = 1;
                }
                else
                {
                    pb.Image = p2.Value;
                    tboard[Convert.ToInt16(pb.Tag)-1] = -1;
                }
                bool winnerIsX = false;
                if(Helper.HasWinner(tboard, out winnerIsX)) //if(board.HasWinner(out  winner))
                {
                    if (winnerIsX)
                    {
                        MessageBox.Show("winner is " + player1.Name);
                    }
                    else
                    {
                        MessageBox.Show("winner is " + player2.Name);
                    }
                    newGame();
                }
                else if (tboard.GetEmptyBoxex().Count == 0)
                {
                    MessageBox.Show("Draw");
                    newGame();
                }
                isXturn = !isXturn;
            }
            //pb.Image = global::TicTacToa.WinForm.Properties.Resources.
        }

        void newGame()
        {
            isXturn = false;
            tboard.Clear();
            p1 = new KeyValuePair<Player, Image>(player1, Properties.Resources.x);
            p2 = new KeyValuePair<Player, Image>(player2, Properties.Resources.o);
            foreach (PictureBox pb in flowLayoutPanel1.Controls)
            {
                pb.Image = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newGame();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var i = Helper.GetBestMove(tboard, true);
            label1.Text = i.ToString();
        }
    }
}
