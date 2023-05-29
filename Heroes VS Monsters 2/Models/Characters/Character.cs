using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heroes_VS_Monsters_2;

namespace Heroes_VS_Monsters_2.Models.Characters
{
    public abstract class Character
    {
        public event Action<Character> OnDeath;
        private int Endurence { get; set; }
        private int Strength { get; set; }
        private int _hitPoints { get; set; }
        public int HitPoints
        { 
            get
            {
                return _hitPoints;
            } 
            set
            {
                if (value <= 0)
                {
                    Die();
                    Console.WriteLine($"{Name} est mort");
                    _hitPoints = 0;
                }
                else
                {
                    _hitPoints = value;
                }
            }
        }
        public string Name { get; set; }
        public char Letter { get; set; }
        public (int y, int x) Pos { get; set; }
        public Character(int hitPoints, Character[,] gameBoard, Board board)
        {
            Dice diceSix = new Dice(1, 6);
            Endurence = diceSix.KeepBestTrhows(4, 3);
            Strength = diceSix.KeepBestTrhows(4, 3);
            _hitPoints = ApplyModifier(hitPoints, Endurence);
            Pos = board.GetPosition();
        }
        public void Hit(Character target)
        {
            Dice diceFour = new Dice(1, 4);
            int lifeToRemove = ApplyModifier(diceFour.Throw(), Strength);
            Console.WriteLine($"{Name} attaque {target.Name} de {lifeToRemove} le laissant à {target.HitPoints}");
            target.HitPoints -= lifeToRemove;
        }
        private int ApplyModifier(int baseStat, int modifierStat)
        {
            int modifier;
            switch (modifierStat)
            {
                case < 5:
                    modifier = -1;
                    break;
                case < 10:
                    modifier = 0;
                    break;
                case < 15:
                    modifier = +1;
                    break;
                case >= 15:
                    modifier = +2;
                    break;
                default: throw new Exception($"Modifier application failed : Stat number = {modifierStat}");
            }
            return baseStat + modifier;
        }
        protected void Die()
        {
            OnDeath?.Invoke(this);
        }
    }
}
