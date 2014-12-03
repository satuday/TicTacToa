using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToa
{
    public class Game
    {

    }

    public class Player
    {
        public 
    }

    public class Board
    {
        List<int> availableBox;
        List<Tuple<int, int, int>> winners;
        
        
        public Board()
        {
            
            
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
            winners.Add(new Tuple<int, int, int>(3, 6, 3));

            winners.Add(new Tuple<int, int, int>(1, 5, 9));       
            winners.Add(new Tuple<int, int, int>(3, 5, 7));


        }

    }

}
