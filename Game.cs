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
        public List<int> Moves;
        public Player()
        {
            Moves = new List<int>();
        }

        public void NextMove(int index)
        {
            Moves.Add(index);
        }
    }

    public class AIPlayer
    {
        public List<int> Moves;
        public List<int> OpponentMoves;
        public AIPlayer()
        {
            OpponentMoves = new List<int>();
            Moves = new List<int>();
        }

        public void GetNextMove()
        {

        }

    }

    public class Board
    {
        public List<int> availableBox;
        List<Tuple<int, int, int>> winners;        
        
        public Board()
        {
            initBoard();   
        }
        public void Clear()
        {
            initBoard();
        }

        public void SetNextMove(int index)
        {
            availableBox.Remove(index);
        }

        public List<int> GetAvailableBox()
        {
            return availableBox;
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
            availableBox = new List<int>();
            availableBox.Add(1);
            availableBox.Add(2);
            availableBox.Add(3);
            availableBox.Add(4);
            availableBox.Add(5);
            availableBox.Add(6);
            availableBox.Add(7);
            availableBox.Add(8);
            availableBox.Add(9);

            winners = new List<Tuple<int, int, int>>(9);
            winners.Add(new Tuple<int, int, int>(1, 2, 3));
            winners.Add(new Tuple<int, int, int>(4, 5, 6));
            winners.Add(new Tuple<int, int, int>(7, 8, 9));

            winners.Add(new Tuple<int, int, int>(1, 4, 7));
            winners.Add(new Tuple<int, int, int>(2, 5, 8));
            winners.Add(new Tuple<int, int, int>(3, 6, 9));

            winners.Add(new Tuple<int, int, int>(1, 5, 9));       
            winners.Add(new Tuple<int, int, int>(3, 5, 7));
        }
    }
}
