using Core.Utilities;
using System.Collections;
using UnityEngine;

namespace Codexus.Maze
{
    public class Maze : MonoBehaviour
    {
        [SerializeField] private MazeGenerator mazeGenerator;
        [SerializeField] private MazeCell mazeCellPrefab;

        // I'm using the pool for holding the cells, but the prefab is marked as static for optimization purpouse.
        private Pool<MazeCell> pool;
        private DirectionFlag[,] grid;

        private void Start()
        {
            pool = new Pool<MazeCell>(mazeCellPrefab, transform);
            grid = mazeGenerator.Generate();
            CreateVisualMaze(grid);
        }

        private void CreateVisualMaze(DirectionFlag[,] grid)
        {
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    pool.Spawn(new Vector3(-i, 0, j), Quaternion.identity).Initialize(grid[i,j]);
                }
            }
        }
    }
}