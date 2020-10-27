using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class Player
    {
        private readonly string _name;

        public Player(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }
    }
}