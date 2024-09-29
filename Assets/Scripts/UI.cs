using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOTS2D
{
    public class UI : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DOTSEventListener.OnScoreChanged += UpdateScoreDisplay;
        }

        private void OnDestroy()
        {
            DOTSEventListener.OnScoreChanged -= UpdateScoreDisplay;
        }

        void UpdateScoreDisplay(int score)
        {
            Debug.Log($"New score UI {score}");
        }
    }
}
