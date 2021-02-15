using System.Collections.Generic;

namespace Codexus.Maze
{
    public static class DirectionExtensions
    {
        // Holds the flag for all directions
        // It will help us to tell if the traversed cell wasn't visited before
        public const DirectionFlag ALL_DIRECTIONS = DirectionFlag.E | DirectionFlag.N | DirectionFlag.S | DirectionFlag.W;

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
        /// Returns x index to traverse
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static int DirectionXIndex(this DirectionFlag direction)
        {
            return DirectionX[direction];
        }

        /// <summary>
        /// Returns y index to traverse
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static int DirectionYIndex(this DirectionFlag direction)
        {
            return DirectionY[direction];
        }

        // We do have directionFlag.HasFlag but we want to avoid it since it is much more resource-intensive including boxing,
        // therefore we will use good'ol bitwise implementation.
        public static bool BitwiseHasFlag(this DirectionFlag direction, DirectionFlag flag)
        {
            return (direction & flag) != 0;
        }
    }
}