
using System.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;



namespace DOTS2D
{
    public delegate void OnScoreChange(int newScore);

    [BurstCompile]
    public partial struct GameStateJob : IJobEntity
    {
        // Store updated score

        public NativeArray<int> scoreChanged; // Store the updated score
        public int lastScore;
       
        //public OnScoreChange ScoreChanged;

        public void Execute(ref GameStateComponent gameState)
        {
            if (gameState.score != lastScore)
            {
                scoreChanged[0] = gameState.score;
            }
            else
            {
                scoreChanged[0] = -1;
            }
        }
    }
}
