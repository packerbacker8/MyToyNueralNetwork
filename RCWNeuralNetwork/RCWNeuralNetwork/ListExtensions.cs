using System;
using System.Collections.Generic;

namespace RCWNeuralNetwork
{
    public static class ListExtensions
    {
        private static Random rand = new Random();
        public static void Shuffle<T>(this List<T> list)
        {
            int amt = list.Count;
            while (amt > 1)
            {
                --amt;
                int swap = rand.Next(amt + 1);
                T val = list[swap];
                list[swap] = list[amt];
                list[amt] = val;
            }
        }
    }
}
