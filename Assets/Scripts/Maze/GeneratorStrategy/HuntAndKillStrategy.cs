using UnityEngine;

namespace Codexus.Maze
{
    /// <summary>
    /// An implementation of the "Hunt and Kill" algorithm. This is fairly
    /// similar to the recursive backtracking algorithm, except that there
    /// is no recursion, and it doesn't backtrack. :) The algorithm can
    /// get a little slow towards the end, where the "hunt" phase has to
    /// search over nearly the entire field to find a candidate cell, but
    /// it's guaranteed to finish.
    /// 
    /// 1. Choose a starting location.
    /// 2. Perform a random walk, carving passages to unvisited neighbors, until the current cell has no unvisited neighbors.
    /// 3. Enter �hunt� mode, where you scan the grid looking for an unvisited cell that is adjacent to a visited cell.If found, carve a passage between the two and let the 
    ///     formerly unvisited cell be the new starting location.
    /// 4. Repeat steps 2 and 3 until the hunt mode scans the entire grid and finds no unvisited cells.

    /// </summary>
    public class HuntAndKillStrategy : IMazeGenerationStrategy
    {
        DirectionFlag[] directions = MazeExtensions.GetValues<DirectionFlag>();

        public DirectionFlag[,] Generate(Vector2Int dimension)
        {
            DirectionFlag[,] grid = CreateGrid(dimension);

            // init the position with random position
            Vector2Int? position = new Vector2Int(Random.Range(0, dimension.y), Random.Range(0, dimension.x));

            while (position.HasValue)
            {
                position = Walk(position.Value, grid);
                position = Hunt(grid);
            }

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

        /// <summary>
        /// Perform a random walk, carving passages to unvisited neighbors, until the current cell has no unvisited neighbors.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="grid"></param>
        /// <returns>Nullable type of Vector2Int, it doesn't create boxing issue: https://stackoverflow.com/a/3775643 </returns>
        private Vector2Int? Walk(Vector2Int position, DirectionFlag[,] grid)
        {
            directions.Shuffle();

            for (int i = 0; i < directions.Length; i++)
            {
                Vector2Int newPosition = new Vector2Int(position.x + directions[i].DirectionXIndex(), position.y + directions[i].DirectionYIndex());
                if (IsValid(newPosition, grid) && !IsVisited(newPosition, grid))
                {
                    DirectionFlag dir = directions[i];
                    grid[position.y, position.x] = grid[position.y, position.x] ^ directions[i]; // remove the wall of the current cell
                    grid[newPosition.y, newPosition.x] = grid[newPosition.y, newPosition.x] ^ directions[i].OppositeDirection(); // remove the wall from the next cell
                    return newPosition;
                }
            }
            return null;
        }

        /// <summary>
        /// Scan the grid looking for an unvisited cell that is adjacent to a visited cell.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="grid"></param>
        private Vector2Int? Hunt(DirectionFlag[,] grid)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    Vector2Int currentPosition = new Vector2Int(x , y);

                    // check if the field was visited. If not then try to find visited neighbors.
                    if ( !IsVisited(currentPosition, grid))
                    {
                        for (int k = 0; k < directions.Length; k++)
                        {
                            Vector2Int newPosition = new Vector2Int(currentPosition.x + directions[k].DirectionXIndex(), currentPosition.y + directions[k].DirectionYIndex());
                            if (IsValid(newPosition, grid) && IsVisited(newPosition, grid))
                            {
                                DirectionFlag dir = directions[k];
                                grid[currentPosition.y, currentPosition.x] = grid[currentPosition.y, currentPosition.x] ^ directions[k]; // remove the wall of the current cell
                                grid[newPosition.y, newPosition.x] = grid[newPosition.y, newPosition.x] ^ directions[k].OppositeDirection(); // remove the wall from the next cell
                                return newPosition;
                            }
                        }
                    }
                }
            }

            return null;
        }

        private bool IsValid(Vector2Int position, DirectionFlag[,] grid)
        {
            return
                position.y >= 0 && position.y <= grid.GetLength(0) - 1 && // check if we are 
                position.x >= 0 && position.x <= grid.GetLength(1) - 1;
        }

        private bool IsVisited(Vector2Int position, DirectionFlag[,] grid)
        {
            return grid[position.y, position.x] != DirectionExtensions.ALL_DIRECTIONS;
        }

    }
}