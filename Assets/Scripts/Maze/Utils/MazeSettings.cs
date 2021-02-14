using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    [CreateAssetMenu(menuName ="Maze/MazeSettings", fileName = "MazeSettings")]
    public class MazeSettings : ScriptableObject
    {
        public Vector2Int MazeDimension;
        public GenerationStrategyType GenerationStrategyType;

        // just some sanity checks
        private void OnValidate()
        {
            MazeDimension.x = Math.Max(0, MazeDimension.x);
            MazeDimension.y = Math.Max(0, MazeDimension.y);
        }
    }
}