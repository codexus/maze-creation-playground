using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codexus.Maze
{
    public class MazeCell : MonoBehaviour
    {
        [SerializeField] private Transform wallE;
        [SerializeField] private Transform wallW;
        [SerializeField] private Transform wallN;
        [SerializeField] private Transform wallS;

        public void Initialize(DirectionFlag directionFlag)
        {
            wallE.gameObject.SetActive(directionFlag.BitwiseHasFlag(DirectionFlag.E));
            wallW.gameObject.SetActive(directionFlag.BitwiseHasFlag(DirectionFlag.W));
            wallN.gameObject.SetActive(directionFlag.BitwiseHasFlag(DirectionFlag.N));
            wallS.gameObject.SetActive(directionFlag.BitwiseHasFlag(DirectionFlag.S));
        }
    }
}