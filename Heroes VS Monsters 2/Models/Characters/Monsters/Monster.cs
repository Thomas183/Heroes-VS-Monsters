using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_VS_Monsters_2.Models.Characters.Monsters
{
    internal abstract class Monster : Character
    {
        public Monster(int hitPoints, Character[,] gameBoard, Board board) : base(hitPoints, gameBoard, board)
        {
        }
    }
}
