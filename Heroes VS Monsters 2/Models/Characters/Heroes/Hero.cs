using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heroes_VS_Monsters_2.Enumerations;
using Heroes_VS_Monsters_2.Inerfaces;
using Heroes_VS_Monsters_2.Models.Characters.Monsters;

namespace Heroes_VS_Monsters_2.Models.Characters.Heroes
{
    internal abstract class Hero : Character
    {
        public Dictionary<string, int> Inventory { get; set; }
        public int MaxHp { get; set; }
        public Hero(int hitPoints, Character[,] gameBoard, Board board) : base(hitPoints, gameBoard, board)
        {
            Inventory = new Dictionary<string, int>();
            foreach (Items item in Enum.GetValues(typeof(Items)))
            {
                Inventory.Add(item.ToString(), 0);
            }
            MaxHp = HitPoints;
            Letter = 'H';
        }
        // Add "AddLootToInventory" function to Even
        public void SubscribeToMonster(Monster monster)
        {
            monster.OnDeath += AddLootToInventory;
        }
        // Add loot to inventory depending of the type of monster
        private void AddLootToInventory(Character monster)
        {
            if (monster is IGold goldMonster)
            {
                Console.WriteLine($"{monster.Name} dropped {goldMonster.Gold} Gold");
                Inventory["Gold"] += goldMonster.Gold;
            }
            if (monster is IHide hideMonster)
            {
                Console.WriteLine($"{monster.Name} dropped {hideMonster.Hide} Hide");
                Inventory["Hide"] += hideMonster.Hide;
            }
        }
    }
}