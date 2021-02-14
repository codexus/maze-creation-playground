using UnityEngine;

namespace Codexus.Maze
{
    public class Maze : MonoBehaviour
    {
        [SerializeField] private MazeGenerator mazeGenerator;
        [SerializeField] private MazeCell mazeCellPrefab;

        private void Start()
        {
            mazeGenerator.Generate();
        }
    }
}