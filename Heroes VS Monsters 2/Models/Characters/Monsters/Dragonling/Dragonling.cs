using Heroes_VS_Monsters_2.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_VS_Monsters_2.Models.Characters.Monsters.Dragonling
{
    internal class Dragonling : Monster, IHide, IGold
    {
        public int Hide { get; set; }
        public int Gold { get; set; }
        public Dragonling(int hitPoints, Character[,] gameBoard, Board board) : base(hitPoints, gameBoard, board)
        {
            Dice diceFour = new Dice(1, 4);
            Dice diceSix = new Dice(1, 6);
            Hide = diceFour.Throw();
            Gold = diceSix.Throw();
            Name = "Dragonling";
            Letter = 'D';
            Endurence += 1;
        }
    }
}
