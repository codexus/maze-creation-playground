using Core.Utilities;
using System.Collections;
using UnityEngine;

namespace Codexus.Maze
{
    public class Maze : MonoBehaviour
    {
        [SerializeField] private MazeGenerator mazeGenerator;
        [SerializeField] private MazeCell mazeCellPrefab;

        private Pool<MazeCell> pool;
        private DirectionFlag[,] grid;

        private void Start()
        {
            pool = new Pool<MazeCell>(mazeCellPrefab, transform);
            grid = mazeGenerator.Generate();
            StartCoroutine(CreateVisualMazeStop(grid));
            //CreateVisualMaze(grid);
        }

        private IEnumerator CreateVisualMazeStop(DirectionFlag[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    pool.Spawn(new Vector3(j, 0, -i), Quaternion.identity).Initialize(grid[i, j]);
                    Debug.Log(grid[i, i]);
                    yield return new WaitForSeconds(1f);
                }
            }
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