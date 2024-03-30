using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchColorGame
{
    public class ProgressIndicator : MonoBehaviour
    {
        [SerializeField] private GameObject[] _progresIndicators;


        private void Start()
        {
            DuckGame.OnScoreUpdated += UpdateProgress;
            DuckGame.OnGameStarted += ResetProgress;
        }

        private void OnDestroy()
        {
            DuckGame.OnScoreUpdated -= UpdateProgress;
            DuckGame.OnGameStarted -= ResetProgress;
        }

        private void UpdateProgress(int score)
        {
            _progresIndicators[score - 1].SetActive(true);
        }

        private void ResetProgress()
        {
            foreach (var indicator in _progresIndicators)
            {
                indicator.SetActive(false);
            }
        } 
    }
}
