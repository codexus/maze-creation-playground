using System.Collections.Generic;

namespace Codexus.Maze
{
    public static class DirectionExtensions
    {
        private static Dictionary<DirectionFlag, int> DirectionX = new Dictionary<DirectionFlag, int>
        {
            { DirectionFlag.E, 1 },
            { DirectionFlag.W, -1},
            { DirectionFlag.N, 0 },
            { DirectionFlag.S, 0 },
        };

        private static Dictionary<DirectionFlag, int> DirectionY = new Dictionary<DirectionFlag, int>
        {
            { DirectionFlag.E, 0 },
            { DirectionFlag.W, 0 },
            { DirectionFlag.N, -1 },
            { DirectionFlag.S, 1 },
        };

        private static Dictionary<DirectionFlag, DirectionFlag> Opposite = new Dictionary<DirectionFlag, DirectionFlag>
        {
            { DirectionFlag.E, DirectionFlag.W },
            { DirectionFlag.W, DirectionFlag.E },
            { DirectionFlag.N, DirectionFlag.S },
            { DirectionFlag.S, DirectionFlag.N },
        };

        /// <summary>
        /// Returns the opposite direction.
        /// </summary>
        /// <param name="direction">current direction</param>
        /// <returns>Opposite direction</returns>
        public static DirectionFlag OppositeDirection(this DirectionFlag direction)
        {
            return Opposite[direction];
        }

        /// <summary>
        /// Returns X index to traverse
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static int DirectionXIndex(this DirectionFlag direction)
        {
            return DirectionX[direction];
        }

        public static int DirectionYIndex(this DirectionFlag direction)
        {
            return DirectionY[direction];
        }
    }
}