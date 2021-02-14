using System;
using System.Collections;
using System.Collections.Generic;



namespace Maze
{
    /// <summary>
    /// Extension methods for maze generation.
    /// </summary>
    public static class MazeExtensions
    {
        private static Random rng = new Random();

        /// <summary>
        /// Shuffle List
        /// </summary>
        /// <see cref="https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle"/>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}