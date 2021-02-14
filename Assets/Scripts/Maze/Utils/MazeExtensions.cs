using System;
using System.Collections.Generic;

namespace Codexus.Maze
{
    /// <summary>
    /// Extension methods for maze generation.
    /// </summary>
    public static class MazeExtensions
    {
        private static Random rng = new Random();

        /// <summary>
        /// Shuffles the values
        /// </summary>
        /// <see cref="https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle"/>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisArray"></param>
        public static void Shuffle<T>(this T[] thisArray)
        {
            int n = thisArray.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = thisArray[k];
                thisArray[k] = thisArray[n];
                thisArray[n] = value;
            }
        }

        /// <summary>
        /// Gets all values from enum and return an array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static T[] GetValues<T>() where T : Enum
        {
           return Enum.GetValues(typeof(T)) as T[];
        }
    }
}