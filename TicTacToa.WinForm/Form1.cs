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

        TicTacToaBoard tboard;

        public Form1()
        {
            InitializeComponent();

            tboard = new TicTacToaBoard();

            player1 = new Player("Player1");
            player2 = new Player("Player2");
            newGame();
            for(int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                if (flowLayoutPanel1.Controls[i] is PictureBox)
                {
                    var pb = flowLayoutPanel1.Controls[i] as PictureBox;
                    pb.Tag = i;
                    pb.Click += pictureBox_Click;
                }
            }
        }

        void setX(int index)
        {
            foreach (PictureBox pb in flowLayoutPanel1.Controls)
            {
                if(Convert.ToInt16(pb.Tag) == index)
                {
                    pictureBox_Click(pb, new EventArgs());
                    break;
                }

            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb.Image == null)
            {
                if (tboard.CurrentPlayer == XO.X)
                {
                    pb.Image = p1.Value;
                    tboard[Convert.ToInt16(pb.Tag)] = XO.X;

                }
                else
                {
                    pb.Image = p2.Value;
                    tboard[Convert.ToInt16(pb.Tag)] = XO.O;
                    var b = Helper.GetBestMove(tboard);
                    label1.Text = "Best for X:" + b;
                    setX(b);

                }
                //tboard.CurrentPlayer = (XO)(Convert.ToInt16(tboard.CurrentPlayer) * -1);
                bool winnerIsX = false;
                if(Helper.HasWinner(tboard, out winnerIsX))
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
                    return;
                }
                else if (tboard.GetEmptyBoxes().Count == 0)
                {
                    MessageBox.Show("Draw");
                    newGame();
                    return;
                }
            }
        }

        void newGame()
        {
            tboard.Clear();
            tboard.CurrentPlayer = XO.O;
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
            int p = -1;
            p = Helper.GetBestMove(tboard);
            label1.Text = p.ToString();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            label1.Text = "Best for X:" + Helper.GetBestMove(tboard);
        }
    }
}
