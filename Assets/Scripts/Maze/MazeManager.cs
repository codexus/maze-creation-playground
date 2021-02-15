using Core.Utilities;
using UnityEngine;

namespace Codexus.Maze
{
    public class MazeManager : MonoBehaviour
    {
        [SerializeField] private MazeGenerator[] mazeGenerators;
        [SerializeField] private MazeCell mazeCellPrefab;

        private Pool<MazeCell> pool;
        private DirectionFlag[,] grid;

        MazeGenerator currentMazeGenerator;

        private void Awake()
        {
            pool = new Pool<MazeCell>(mazeCellPrefab, transform);
        }

        private void CreateVisualMaze(DirectionFlag[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    pool.Spawn(new Vector3(j, 0, -i), Quaternion.identity).Initialize(grid[i, j]);
                }
            }
        }

        public void RegenerateMaze()
        {
            if (mazeGenerators.Length == 0) return;
            if (currentMazeGenerator == null) currentMazeGenerator = mazeGenerators[0];
            pool.ClearPool();
            grid = currentMazeGenerator.Generate();
            CreateVisualMaze(grid);
        }

        public MazeGenerator[] GetMazeGenerators()
        {
            return mazeGenerators;
        }

        public int GetCurrentGeneratorIndex()
        {
            if (currentMazeGenerator == null) currentMazeGenerator = mazeGenerators[0];

            for (int i = 0; i < mazeGenerators.Length; i++)
            {
                if (currentMazeGenerator == mazeGenerators[i]) return i;
            }

            return -1;
        }

        public void SetGenerationMethod(int index)
        {
            currentMazeGenerator = mazeGenerators[index];
        }
    }
}