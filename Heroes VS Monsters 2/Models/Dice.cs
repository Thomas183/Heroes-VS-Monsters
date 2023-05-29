﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_VS_Monsters_2.Models
{
    public class Dice
    {
        private int _min;
        private int _max;
        private Random rnd = new Random();
        public Dice(int min, int max)
        {
            _min = min;
            _max = max;
        }
        public int Throw()
        {
            return rnd.Next(_min, _max+1);
        }
        public int KeepBestTrhows(int throws, int ammountToKeep)
        {
            int score = 0;
            List<int> scores = new List<int>();
            for (int i = 0; i < throws; i++)
            {
                scores.Add(Throw());
            }
            scores.Sort();
            scores.Reverse();
            for (int i = 0; i < ammountToKeep; i++)
            {
                score += scores[i];
            }
            return score;
        }
    }
}
