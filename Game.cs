﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToa
{
    public class Game
    {
        Board board;
        Player p1;
        Player p2;

        public Game(Board board, Player player1, Player player2)
        {
            this.board = board;
            p1 = player1;
            p2 = player2;
        }

        public void NewGame()
        {
            p1.Moves.Clear();
            p2.Moves.Clear();
            board.Clear();
        }
    }

    public class Player
    {
        public string Name{get;set;}
        public List<int> Moves;

        public Player(string name = "Player1")
        {
            Name = name;
            Moves = new List<int>();
        }

        public void NextMove(int index)
        {
            Moves.Add(index);
        }
    }

    public class Board
    {
        List<Tuple<int, int, int>> winners;
        Player p1;
        Player p2;
        bool isP1Turn = true;

        List<int> p1Moves, p2Moves;

        public Board(Player player1, Player player2)
        {
            p1Moves = new List<int>();
            p1 = player1;
            p2 = player2;
        }
        
        public Board():this(new Player(), new Player())
        {
            initBoard();   
        }
        public void Clear()
        {
            initBoard();
        }

        public void SetNextMove(int index)
        {
            isP1Turn = !isP1Turn;
            ///availableBox.Remove(index);
        }

        public bool IsWinner(int[] boxes)
        {
            foreach (var t in winners)
            {
                if (boxes.Contains(t.Item1) && boxes.Contains(t.Item2) && boxes.Contains(t.Item3))
                    return true;
            }
            return false;
        }

        void initBoard()
        {
            winners = new List<Tuple<int, int, int>>(8);
            winners.Add(new Tuple<int, int, int>(1, 2, 3));
            winners.Add(new Tuple<int, int, int>(4, 5, 6));
            winners.Add(new Tuple<int, int, int>(7, 8, 9));

            winners.Add(new Tuple<int, int, int>(1, 4, 7));
            winners.Add(new Tuple<int, int, int>(2, 5, 8));
            winners.Add(new Tuple<int, int, int>(3, 6, 9));

            winners.Add(new Tuple<int, int, int>(1, 5, 9));       
            winners.Add(new Tuple<int, int, int>(3, 5, 7));
        }

        public bool HasWinner(out Player winner)
        {
            winner = null;
            foreach (var t in winners)
            {
                if (p1.Moves.Contains(t.Item1) && p1.Moves.Contains(t.Item2) && p1.Moves.Contains(t.Item3))
                {
                    winner = p1;
                    return true;
                }
                else if (p2.Moves.Contains(t.Item1) && p2.Moves.Contains(t.Item2) && p2.Moves.Contains(t.Item3))
                {
                    winner = p2;
                    return true;
                }
            }
            return false;
        }

        public int GetNextBestMove()
        {
            return 0;
        }
    }

    public static class Helper
    {
        static List<Tuple<int, int, int>> winners;

        static Helper()
        {
            winners = new List<Tuple<int, int, int>>(8);
            winners.Add(new Tuple<int, int, int>(0, 1, 2));
            winners.Add(new Tuple<int, int, int>(3, 4, 5));
            winners.Add(new Tuple<int, int, int>(6, 7, 8));

            winners.Add(new Tuple<int, int, int>(0, 3, 6));
            winners.Add(new Tuple<int, int, int>(1, 4, 7));
            winners.Add(new Tuple<int, int, int>(2, 5, 8));

            winners.Add(new Tuple<int, int, int>(0, 4, 8));
            winners.Add(new Tuple<int, int, int>(2, 4, 6));
        }

        public static bool HasWinner(TicTacToaBoard board, out bool winnerIsX)
        {           
            winnerIsX = false;
            if (board.xPositions.Count < 3 && board.oPositions.Count < 3)
                return false;
            foreach (var t in winners)
            {
                if (board.xPositions.Contains(t.Item1) && board.xPositions.Contains(t.Item2) && board.xPositions.Contains(t.Item3))
                {
                    winnerIsX = true;
                    return true;
                }
                else if (board.oPositions.Contains(t.Item1) && board.oPositions.Contains(t.Item2) && board.oPositions.Contains(t.Item3))
                {
                    winnerIsX = false;
                    return true;
                }

            }
            return false;
        }

        public static int GetBoardValue2(TicTacToaBoard board)
        {
            int i = 0;
            foreach (var line in winners)
            {
                i += getLineValue(new XO[] { board[line.Item1], board[line.Item2], board[line.Item3] });
            }

            return i;
        }

        static int getLineValue(XO[] line)
        {
            if (line[0] == XO.X && line[1] == XO.X && line[2] == XO.X)
            {
                return 100;
            }
            else if (line[0] == XO.O && line[1] == XO.O && line[2] == XO.O)
            {
                return -100;
            }

            if ((line[0] == XO.X && line[1] == XO.X && line[2] == XO.Empty) ||
                (line[0] == XO.X && line[1] == XO.Empty && line[2] == XO.X) ||
                (line[0] == XO.Empty && line[1] == XO.X && line[2] == XO.X))
            {
                return 10;
            }
            else if ((line[0] == XO.O && line[1] == XO.O && line[2] == XO.Empty) ||
                    (line[0] == XO.O && line[1] == XO.Empty && line[2] == XO.O) ||
                    (line[0] == XO.Empty && line[1] == XO.O && line[2] == XO.O))
            {
                return -10;
            }

            return 0;

        }

        public static int GetBoardValue(TicTacToaBoard board)
        {
            int val = 0;
            var xPos = board.xPositions;
            var oPos = board.oPositions;
            foreach (var t in winners)
            {
                if (xPos.Contains(t.Item1) && xPos.Contains(t.Item2) && xPos.Contains(t.Item3))
                {
                    return 100;
                }
                else if (oPos.Contains(t.Item1) && oPos.Contains(t.Item2) && oPos.Contains(t.Item3))
                {
                    return -100;
                }

                if (xPos.Contains(t.Item1) && xPos.Contains(t.Item2))
                    val +=10;
                if (xPos.Contains(t.Item2) && xPos.Contains(t.Item3))
                    val +=10;
                if (xPos.Contains(t.Item3) && xPos.Contains(t.Item1))
                    val = +10;

                if (oPos.Contains(t.Item1) && oPos.Contains(t.Item2))
                    val -= 10;
                if (oPos.Contains(t.Item2) && oPos.Contains(t.Item3))
                    val -= -10;
                if (oPos.Contains(t.Item3) && oPos.Contains(t.Item1))
                    val -= 10;
            }

            return val;
        }

        public static int GetBestMove(TicTacToaBoard board)
        {
            int p = -1;
            var boxes = board.GetEmptyBoxes();
            int bestVal = -100;

            foreach (var box in boxes)
            {
                var tb = board.GetBoardAfterNewMove(box);
                var val = GetMinimax(tb, 10, false);
                System.Diagnostics.Debug.WriteLine(box.ToString() + ":" + val.ToString());
                if (val > bestVal)
                {
                    bestVal = val;
                    p = box;
                }               
            }
            System.Diagnostics.Debug.WriteLine(" ");
            return p;
        }

        public static int GetMinimax(TicTacToaBoard board, int depth, bool maximizingPlayer)
        {
            bool xWin;
            var boxes = board.GetEmptyBoxes();

            if(HasWinner(board, out xWin) || depth == 0 || boxes.Count == 0)
            {
                return GetBoardValue2(board);
            }

            if (maximizingPlayer)
            {
                int bestVal = -100;
                foreach (var box in boxes)
                {
                    var b= board.GetBoardAfterNewMove(box);
                    var val = GetMinimax(b, depth - 1, false);
                    bestVal = Math.Max(bestVal, val);
                }
                return bestVal;
            }
            else
            {
                int bestVal = 100;
                foreach (var box in boxes)
                {
                    var b = board.GetBoardAfterNewMove(box);
                    var val = GetMinimax(b, depth - 1, true);
                    bestVal = Math.Min(bestVal, val);
                }
                return bestVal;
            }
        }
    }

    public enum XO
    {
        X = 1,
        O = -1,
        Empty = 0
    }

    public class TicTacToaBoard
    {
        XO[] boxes;
        //public bool isXturn = false;
        public int Depth { get; set; }
        public XO CurrentPlayer { get; set; }

        public List<int> xPositions { get 
        {
            List<int> os = new List<int>();
            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i] == XO.X)
                    os.Add(i);
            }
            return os;
        } }
        public List<int> oPositions { get 
        {
            List<int> os = new List<int>();
            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i] == XO.O)
                    os.Add(i);
            }
            return os;
        }}

        public void Clear()
        {
            init();
        }

        void init()
        {
            boxes[0] = 0;
            boxes[1] = 0;
            boxes[2] = 0;
            boxes[3] = 0;
            boxes[4] = 0;
            boxes[5] = 0;
            boxes[6] = 0;
            boxes[7] = 0;
            boxes[8] = 0;
        }

        public TicTacToaBoard()
        {
            CurrentPlayer = XO.O;
            boxes = new XO[9];
            init();
        }

        public List<int> GetEmptyBoxes()
        {
            List<int> os = new List<int>();
            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i] == 0)
                    os.Add(i);
            }
            return os;
        }

        public XO this[int i]
        {
            get
            {
                return boxes[i];
            }
            set
            {
                this.CurrentPlayer = value > 0 ? XO.O : XO.X;
                boxes[i] = value;
            }
        }

        public bool HasWinner(out bool xWin)
        {
            return Helper.HasWinner(this, out xWin);
        }

        public TicTacToaBoard GetBoardAfterNewMove(int boxIndex)
        {
            TicTacToaBoard newBoard = new TicTacToaBoard();
            //newBoard.CurrentPlayer = (XO)(Convert.ToInt16(this.CurrentPlayer) * -1);
            newBoard.boxes = (XO[])this.boxes.Clone();
            newBoard.Depth = this.Depth + 1;
            newBoard[boxIndex] = this.CurrentPlayer;
            return newBoard;
        }
    }
}
