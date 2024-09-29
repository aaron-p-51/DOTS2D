using System.Diagnostics;
using Unity.Entities;
using Unity.Collections;


namespace DOTS2D
{
    public partial class GameStateSystem : SystemBase
    {
        //public delegate void OnScoreChange(int newScore);
        public static event OnScoreChange ScoreChanged;

        private int lastScore = -1;

        protected override void OnUpdate()
        {

            //var scoreChanged = new NativeArray<int>(1, Allocator.TempJob);
            //var job = new GameStateJob
            //{
            //    lastScore = this.lastScore,
            //    scoreChanged = scoreChanged
            //};

            //job.Run();

            //int newScore = scoreChanged[0];
            //if (newScore != -1 && newScore != lastScore)
            //{
            //    lastScore = newScore;
            //    ScoreChanged?.Invoke(job.lastScore);
            //}



        }
    }
}
