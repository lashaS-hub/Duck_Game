using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuizGame
{
    public class AnswerButton : MonoBehaviour
    {
        [SerializeField] private Button _answerButton;
        [SerializeField] private TMP_Text _answerText;
        [SerializeField] private Image _decorator;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _correctColor;
        [SerializeField] private Color _incorrectColor;

        public Button AnswerBtn => _answerButton;

        private bool _isCorrect;
        private Action<bool> _onAnswerClicked;
        private AnswerData _answerData;

        private void Start()
        {
            _answerButton.onClick.AddListener(OnAnswerClicked);
        }


        internal void Initialize(AnswerData answerData, Action<bool> onAnswerClicked)
        {
            gameObject.SetActive(true);
            _answerData = answerData;
            _decorator.color = _defaultColor;
            _answerText.text = _answerData.AnswerText;
            _isCorrect = _answerData.AnswerType == AnswerType.Correct;
            _onAnswerClicked = onAnswerClicked;
        }


        public void OnAnswerClicked()
        {
            _decorator.color = _isCorrect ? _correctColor : _incorrectColor;
            _onAnswerClicked?.Invoke(_isCorrect);
        }
    }
}
