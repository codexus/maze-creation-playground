using UnityEngine;

namespace Codexus.Maze
{
    public class RecursiveBacktrackingStrategy : IMazeGenerationStrategy
    {
        // Holds the flag for all directions
        // It will help us to tell if the traversed cell wasn't visited before
        public const DirectionFlag ALL_DIRECTIONS = DirectionFlag.E | DirectionFlag.N | DirectionFlag.S | DirectionFlag.W;

        // Cached directions for 
        DirectionFlag[] directions = MazeExtensions.GetValues<DirectionFlag>();
        
        public DirectionFlag[,] Generate(Vector2Int dimension)
        {
            DirectionFlag[,] grid = CreateGrid(dimension);
            Vector2Int startPosition = new Vector2Int(0, 0);

            Carve(startPosition, grid);
            return grid;
            //Print(grid);
        }

        DirectionFlag[,] CreateGrid(Vector2Int dimension)
        {
            DirectionFlag[,] grid = new DirectionFlag[dimension.x, dimension.y];

            for (int i = 0; i < dimension.y; i++)
            {
                for (int j = 0; j < dimension.x; j++)
                {
                    grid[i, j] = ALL_DIRECTIONS; // initialize 
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
                    grid[startPosition.y, startPosition.x] = grid[startPosition.y, startPosition.x] ^ directions[i]; // remove the wall of the current cell
                    grid[newPosition.y, newPosition.x] = grid[startPosition.y, startPosition.x] ^ directions[i].OppositeDirection(); // remove the wall from the next cell
                    Carve(newPosition, grid);
                }
            }
        }

        private bool IsValid(Vector2Int position, DirectionFlag[,] grid)
        {
            return
                position.y >= 0 && position.y <= grid.GetLength(0) - 1 &&
                position.x >= 0 && position.x <= grid.GetLength(1) - 1 &&
                grid[position.y, position.x] == ALL_DIRECTIONS;
        }

        private void Print(DirectionFlag[,] grid)
        {
            string s = "";
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    s += grid[i, j].ToString() + "|"; // initialize 
                }
                s += "/n";
            }

            Debug.Log(s);
        }


    }
}