using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuizGame
{
    public class GameEndPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _messageText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _toLobbyButton;
        private Action _onRestartClicked;

        private void Awake()
        {
            _restartButton.onClick.AddListener(() => _onRestartClicked?.Invoke());
            _toLobbyButton.onClick.AddListener(() => SceneController.Singleton.LoadLobby());
        }

        internal void Initialize(int correctAnswerCount, int maxAnswerCount, Action onRestartClicked)
        {
            gameObject.SetActive(true);
            var result = GetMessageAndColor(correctAnswerCount);

            _onRestartClicked = onRestartClicked;
            _messageText.text = result.message + "\n" + correctAnswerCount + " / " + maxAnswerCount;
            _messageText.color = result.color;

        }

        private (string message, Color color) GetMessageAndColor(int correctAnswerCount)
        {
            string message;
            Color color;

            switch (correctAnswerCount)
            {
                case var count when count >= 0 && count <= 1:
                    message = "Terrible";
                    color = Color.red;
                    break;
                case var count when count >= 2 && count <= 4:
                    message = "Good Job";
                    color = Color.yellow;
                    break;
                default:
                    message = "Great Job";
                    color = Color.green;
                    break;
            }

            return (message, color);
        }


    }
}
