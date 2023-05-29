using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Heroes_VS_Monsters_2.Models.Characters;
using Heroes_VS_Monsters_2.Models.Characters.Monsters;
using Heroes_VS_Monsters_2.Models.Characters.Monsters.Dragonling;
using Heroes_VS_Monsters_2.Models.Characters.Monsters.Orc;
using Heroes_VS_Monsters_2.Models.Characters.Monsters.Wolf;

namespace Heroes_VS_Monsters_2.Models
{
    public class Board
    {
        public Character?[,] board { get; set; }

        public Board(int height, int width)
        {
            board = new Character?[height, width];
        }
        // Returns true if given position is not within two cases of another Character
        public bool IsPositionAvailable((int y, int x) position)
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);
            // Relative X / Y positions
            (int rY, int rX)[] relativePosition = new (int, int)[]
            {
                (1, 0), // One Up
                (2, 0), // Two Up
                (-1, 0), // One Down
                (-2, 0), // Two Down
                (0, 1), // One Right
                (0, 2), // Two Right
                (0, -1), // One Left
                (0, -2), // Two Left
            };
            // Return false if case occupied by monster
            if (board[position.y, position.x] != null)
            {
                return false;
            }
            foreach ((int x, int y) pos in relativePosition)
            {
                int rX = position.x + pos.x;
                int rY = position.y + pos.y;

                // Continue loop when on a border
                if (IsPositionBorder((rY, rX)))
                {
                    continue;
                }
                // Return false if relative position is occupied
                if (board[rY, rX] != null)
                {
                    return false;
                }
            }
            return true;
        }
        // Return true if given position is not outside the border
        public bool IsPositionBorder((int y, int x) pos)
        {
            int boardHeight = board.GetLength(0);
            int boardWidth = board.GetLength(1);
            if (pos.x < 0 || pos.x >= boardWidth || pos.y < 0 || pos.y >= boardHeight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // Returns a position that is not within two cases of another Character or outside the border
        public (int y, int x) GetPosition()
        {
            Dice diceFifteen = new Dice(0, 14);
            (int y, int x) coordinates = (diceFifteen.Throw(), diceFifteen.Throw());

            while (!IsPositionAvailable(coordinates))
            {
                coordinates = (diceFifteen.Throw(), diceFifteen.Throw());
            }
            return coordinates;
        }
        // Populate board with monsters and return the amount added
        public int PopulateBoard()
        {
            Random rnd = new Random();
            int monstersAmount = rnd.Next(10, 26);
            for(int i = 0; i < monstersAmount; i++)
            {
                Monster monster = null;
                switch (rnd.Next(0, 3))
                {
                    case 0:
                        monster = new Orc(10, board, this);
                        break;
                    case 1:
                        monster = new Wolf(10, board, this);
                        break;
                    case 2:
                        monster = new Dragonling(10, board, this);
                        break;
                }
                board[monster!.Pos.y, monster.Pos.x] = monster;
            }
            return monstersAmount;
        }
        // Move the given Character in the given New Position and if oldPosition is passed, reset that position
        public void Move(Character character, (int y, int x) newPosition, (int y, int x)? oldPosition = null)
        {
            if(!IsPositionBorder(newPosition))
            {
                character.Pos = newPosition;
                board[newPosition.y, newPosition.x] = character;
                if(oldPosition != null)
                {
                    board[oldPosition.Value.y, oldPosition.Value.x] = null;
                }
            }
            else
            {
                Console.WriteLine($"Couldn't place character at {newPosition}");
            }
        }
    }
}
