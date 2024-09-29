using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOTS2D
{
    public class DOTSEventListener : MonoBehaviour
    {
        public static Action<int> OnScoreChanged;


        //IEnumerator Start()
        //{
        //    while (GameStateSystem.ScoreChanged == null)
        //    {
        //        yield return null;
        //    }


        //}
        private void Start()
        {
            GameStateSystem.ScoreChanged += HandleScoreChanged;
        }

        private void OnDisable()
        {
            GameStateSystem.ScoreChanged -= HandleScoreChanged;
        }

        private void HandleScoreChanged(int newScore)
        {
            OnScoreChanged?.Invoke(newScore);
        }

    }
}
