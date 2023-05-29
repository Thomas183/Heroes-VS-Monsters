using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_VS_Monsters_2.Models.Characters.Heroes.Human
{
    internal class Human : Hero
    {
        public Human(int hitPoints, Character[,] gameBoard, Board board) : base(hitPoints, gameBoard, board)
        {
            Strength += 1;
            Name = "Human";
        }
    }
}
