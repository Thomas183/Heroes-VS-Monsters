using Heroes_VS_Monsters_2.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_VS_Monsters_2.Models.Characters.Monsters.Orc
{
    internal class Orc : Monster, IGold
    {
        public int Gold { get; set; }
        public Orc(int hitPoints, Character[,] gameBoard, Board board) : base(hitPoints, gameBoard, board)
        {
            Dice dice = new Dice(1, 6);
            Gold = dice.Throw();
            Name = "Orc";
            Letter = 'O';
            Strength += 1;
        }
    }
}
