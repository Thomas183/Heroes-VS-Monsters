using Heroes_VS_Monsters_2.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_VS_Monsters_2.Models.Characters.Monsters.Wolf
{
    internal class Wolf : Monster, IHide
    {
        public int Hide { get; set; }
        public Wolf(int hitPoints, Character[,] gameBoard, Board board) : base(hitPoints, gameBoard, board)
        {
            Dice dice = new Dice(1, 4);
            Hide = dice.Throw();
            Name = "Wolf";
            Letter = 'W';
        }
    }
}
