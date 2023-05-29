using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_VS_Monsters_2.Models.Characters.Heroes.Dwarf
{
    internal class Dwarf : Hero
    {
        public Dwarf(int hitPoints, Character[,] gameBoard, Board board) : base(hitPoints, gameBoard, board)
        {
            Endurence += 2;
            Name = "Dwarf";
        }
    }
}
