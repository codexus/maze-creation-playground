using UnityEngine;

namespace Codexus.Maze
{
    /// <summary>
    /// Recursive backtracking algorithm for maze generation. Requires that
    /// the entire maze be stored in memory, but is quite fast.
    ///
    /// 1. Choose a starting point in the field.
    /// 2. Randomly choose a wall at that point and carve a passage through to the adjacent cell, but only if the adjacent cell has not been visited yet. This becomes the new current cell.
    /// 3. If all adjacent cells have been visited, back up to the last cell that has uncarved walls and repeat.
    /// 4. The algorithm ends when the process has backed all the way up to the starting point.

    /// </summary>
    public class RecursiveBacktrackingStrategy : IMazeGenerationStrategy
    {
        // Cached directions
        DirectionFlag[] directions = MazeExtensions.GetValues<DirectionFlag>();
        
        public DirectionFlag[,] Generate(Vector2Int dimension)
        {
            DirectionFlag[,] grid = CreateGrid(dimension);
            Vector2Int startPosition = new Vector2Int(0, 0);

            Carve(startPosition, grid);
            return grid;
        }

        private DirectionFlag[,] CreateGrid(Vector2Int dimension)
        {
            DirectionFlag[,] grid = new DirectionFlag[dimension.y, dimension.x];

            for (int i = 0; i < dimension.y; i++)
            {
                for (int j = 0; j < dimension.x; j++)
                {
                    grid[i, j] = DirectionExtensions.ALL_DIRECTIONS; // initialize 
                }
            }

            return grid;
        }

        private void Carve(Vector2Int startPosition, DirectionFlag[,] grid)
        {
            // We sort the list in random order, so that the path will meander, rather than having a bias in any particular direction.
            directions.Shuffle();

            for (int i = 0; i < directions.Length; i++)
            {
                Vector2Int newPosition = new Vector2Int(startPosition.x + directions[i].DirectionXIndex(), startPosition.y + directions[i].DirectionYIndex());
                if(IsValid(newPosition, grid))
                {
                    DirectionFlag dir = directions[i];
                    grid[startPosition.y, startPosition.x] = grid[startPosition.y, startPosition.x] ^ directions[i]; // remove the wall of the current cell
                    grid[newPosition.y, newPosition.x] = grid[newPosition.y, newPosition.x] ^ directions[i].OppositeDirection(); // remove the wall from the next cell
                    Carve(newPosition, grid);
                }
            }
        }

        private bool IsValid(Vector2Int position, DirectionFlag[,] grid)
        {
            return
                position.y >= 0 && position.y <= grid.GetLength(0) - 1 && // check if we are 
                position.x >= 0 && position.x <= grid.GetLength(1) - 1 &&
                grid[position.y, position.x] == DirectionExtensions.ALL_DIRECTIONS;
        }
    }
}