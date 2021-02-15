using System;
using UnityEngine;

namespace Codexus.Maze
{
    [CreateAssetMenu(menuName = "Maze/MazeGenerator", fileName = "MazeGenerator")]
    public class MazeGenerator : ScriptableObject
    {
        #region Inspector
        [SerializeField] private Vector2Int mazeDimension;
        [SerializeField] private GenerationStrategyType generationStrategyType;
        #endregion

        private IMazeGenerationStrategy mazeGenerationStrategy;

        public GenerationStrategyType GenerationStrategyType => generationStrategyType;

        private IMazeGenerationStrategy GetMazeGenerationStrategy(GenerationStrategyType generationStrategyType)
        {
            switch (generationStrategyType)
            {
                case GenerationStrategyType.RecursiveBacktrackingStrategy:
                    return new RecursiveBacktrackingStrategy();
                case GenerationStrategyType.HuntAndKillStrategy:
                    return new HuntAndKillStrategy();
                default:
                    return new RecursiveBacktrackingStrategy();
            }
        }

        public DirectionFlag[,] Generate()
        {
            return mazeGenerationStrategy.Generate(mazeDimension);
        }

        #region Unity methods

        private void OnValidate()
        {
            // Just some sanity checks to keep the dimension as a postive number.
            mazeDimension.x = Math.Max(1, mazeDimension.x);
            mazeDimension.y = Math.Max(1, mazeDimension.y);

            // Update the mazeGenerationStrategy when value is changed
            mazeGenerationStrategy = null;
            mazeGenerationStrategy = GetMazeGenerationStrategy(generationStrategyType);
        } 

        #endregion
    }
}