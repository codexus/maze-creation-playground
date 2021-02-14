using UnityEngine;

namespace Codexus.Maze
{
    public interface IMazeGenerationStrategy
    {
        DirectionFlag[,] Generate(Vector2Int dimension);
    }
}

