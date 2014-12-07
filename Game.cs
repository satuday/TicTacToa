using System;
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

    public class TicTacToaBoard
    {
        int[] boxes;

        public TicTacToaBoard()
        {
            boxes = new int[9];
        }

        public int this[int i]
        {
            get
            {
                return boxes[i];
            }
            set
            {
                boxes[i] = value;
            }
        }
    }
}
