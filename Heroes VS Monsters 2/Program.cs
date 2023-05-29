using Heroes_VS_Monsters_2.Models;
using Heroes_VS_Monsters_2.Models.Characters;
using Heroes_VS_Monsters_2.Models.Characters.Heroes.Dwarf;
using Heroes_VS_Monsters_2.Models.Characters.Heroes.Human;
using Heroes_VS_Monsters_2.Models.Characters.Heroes;
using Heroes_VS_Monsters_2.Models.Characters.Monsters;
using Heroes_VS_Monsters_2.Models.Characters.Monsters.Wolf;
using Heroes_VS_Monsters_2.Inerfaces;
using System.Runtime.CompilerServices;

namespace Heroes_VS_Monsters_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool GameRunning = true;
            while (GameRunning)
            {
                Console.WriteLine("-Game start-\n");
                Board board = new Board(15, 15);
                // Character Selection
                Hero hero;
                Console.WriteLine("Quelle classe désirez vous jouer ?\n1: Humain\n2: Nain\n");
                switch (GetUserInput(2))
                {
                    case 1:
                        Console.WriteLine("Vous avez choisi l'humain\n");
                        hero = new Human(100, board.board, board);
                        break;
                    case 2:
                        Console.WriteLine("Vous avez choisi le nain\n");
                        hero = new Dwarf(10, board.board, board);
                        break;
                    default: throw new Exception($"Character selection error");
                }
                // Add character to board
                board.Move(hero, (hero.Pos.y, hero.Pos.x));
                // Add monsters to board
                int monstersLeft = board.PopulateBoard();
                Console.ReadKey();

                while (hero.HitPoints > 0 && monstersLeft > 0)
                {
                    DrawBoard(board.board);
                    ReadArrowKey(hero, board);
                    isHeroNextToMonster(hero, board, ref monstersLeft);
                }
                if (monstersLeft == 0)
                {
                    Console.WriteLine("Tout les monstres sont morts, vous avez gagné");
                }
                Console.WriteLine("Voulez-vous rejouer ?\n1: Oui\n2: Non");
                switch(GetUserInput(2))
                {
                    case 1:
                        break;
                    case 2:
                        GameRunning = false;
                        break;
                }
            }
        }
        public static int GetUserInput(int range)
        {
            int result = 0;
            while (!int.TryParse(Console.ReadLine(), out result) || result <= 0 || result > range)
            {
                Console.WriteLine("Mauvaise entrée, réessayez");
            }
            return result;
        }
        private static void DrawBoard(Character[,] board)
        {
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss"));
            for (int y = 0; y < board.GetLength(0); y++)
            {
                for (int x = 0; x < board.GetLength(1); x++)
                {
                    if (board[y, x] is Character)
                    {
                        Console.Write($"[{board[y, x].Letter}]");
                    }
                    else
                    {
                        Console.Write("[#]");
                    }
                }
                Console.WriteLine();
            }
        }
        public static void ReadArrowKey(Character hero, Board board)
        {
            bool isPositionAvailable = false;
            while (!isPositionAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                (int y, int x) newPos = (0, 0);
                switch (keyInfo.Key)
                {
                    case (ConsoleKey.UpArrow):
                        newPos.y = hero.Pos.y - 1;
                        newPos.x = hero.Pos.x;
                        break;
                    case (ConsoleKey.DownArrow):
                        newPos.y = hero.Pos.y + 1;
                        newPos.x = hero.Pos.x;
                        break;
                    case (ConsoleKey.LeftArrow):
                        newPos.y = hero.Pos.y;
                        newPos.x = hero.Pos.x - 1;
                        break;
                    case (ConsoleKey.RightArrow):
                        newPos.y = hero.Pos.y;
                        newPos.x = hero.Pos.x + 1;
                        break;
                    default:
                        Console.WriteLine("Mauvaise entrée, réessayez");
                        break;
                }
                if (!board.IsPositionBorder(newPos))
                {
                    board.Move(hero, newPos, hero.Pos);
                    isPositionAvailable = true;
                }
            }
        }
        public static void isHeroNextToMonster(Hero hero, Board board, ref int monstersLeft)
        {
            (int y, int x)[] relativePosition = new (int y, int x)[]
            {
                (1, 0), // One Up
                (-1, 0), // One Down
                (0, 1), // One Right
                (0, -1), // One Left
            };
            foreach ((int y, int x) rPos in relativePosition)
            {
                int rY = hero.Pos.y + rPos.y;
                int rX = hero.Pos.x + rPos.x;
                if (board.IsPositionBorder((rY, rX))) { continue; }
                if (board.board[rY, rX] != null)
                {
                    fight(hero, board.board[rY, rX]!, board.board!, ref monstersLeft);
                }
            }
        }
        public static void fight(Hero hero, Character monster, Character[,] board, ref int monstersLeft)
        {
            hero.SubscribeToMonster(monster as Monster);
            while (monster.HitPoints > 0 && hero.HitPoints > 0)
            {
                hero.Hit(monster);
                if (monster.HitPoints > 0)
                {
                    monster.Hit(hero);
                }
                Console.ReadKey();
            }
            if (hero.HitPoints <= 0)
            {
                Console.WriteLine("Vous avez perdu");
            }
            if (monster.HitPoints <= 0)
            {
                Console.WriteLine("Vous avez vaincu le monstre, vous vous reposez et récupérez tous vos points de vie");
                monstersLeft--;
                foreach(KeyValuePair<string, int> item in hero.Inventory)
                {
                    Console.WriteLine("Contenu de l'inventaire :");
                    Console.WriteLine($"{item.Key} : {item.Value}");
                }
                hero.HitPoints = hero.MaxHp;
                Console.ReadKey();
            }
        }
    }
}
       
