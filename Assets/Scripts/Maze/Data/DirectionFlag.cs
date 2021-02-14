using System;

namespace Codexus.Maze
{
    /// <summary>
    /// Direction stored as a bit flag.
    /// </summary>
    [Flags]
    public enum DirectionFlag
    {
        N = 1 << 1,
        S = 1 << 2,
        E = 1 << 3,
        W = 1 << 4,
    }
}