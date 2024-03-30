using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace QuizGame
{
    public class QuestionsGame : MonoBehaviour
    {
        [SerializeField] private GameEndPanel _gameEndPanel;
        [SerializeField] private GameObject _questionsPanel;
        [SerializeField] private QuestionData[] _questions;
        [SerializeField] private AnswerButton[] _answerButtons;
        [SerializeField] private TMP_Text _questionText;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private float _delayBetweenQuestions = 2f;

        private int _maxLevel;
        private int _currentLevel;
        private int _correctAnswerCount;
        private bool _isAnswered = false;

        public void StartGame()
        {
            InitializeStage();
            StartCoroutine(StartGameRoutine());
        }

        private IEnumerator StartGameRoutine()
        {
            while (_currentLevel <= _maxLevel)
            {
                InitializationQuestion();
                SetLevelText();
                yield return new WaitUntil(() => _isAnswered);
                _isAnswered = false;
                yield return new WaitForSeconds(_delayBetweenQuestions);
            }

            _questionsPanel.SetActive(false);

            _gameEndPanel.Initialize(_correctAnswerCount, _maxLevel, OnRestartClicked);

        }

        private void InitializationQuestion()
        {
            
            QuestionData questionData = _questions[_currentLevel - 1];
            _questionText.text = questionData.Question;
            for (int i = 0; i < _answerButtons.Length; i++)
            {
                if (i < questionData.Answers.Length && questionData.Answers[i] != null)
                {
                    _answerButtons[i].AnswerBtn.interactable = true;
                    _answerButtons[i].Initialize(questionData.Answers[i], OnAnswerClicked);
                }
                else
                {
                    _answerButtons[i].gameObject.SetActive(false);
                }
            }
        }


        private void OnAnswerClicked(bool isCorrect)
        {
            EnableButtons(false);
            _isAnswered = true;
            _correctAnswerCount += isCorrect ? 1 : 0;
        }

        private void EnableButtons(bool value)
        {
            foreach (var button in _answerButtons)
            {
                button.AnswerBtn.interactable = value;
            }
        }

        private void InitializeStage()
        {
            _correctAnswerCount = 0;
            _maxLevel = _questions.Length;
            _currentLevel = 1;
        }

        private void SetLevelText()
        {
            string levelString = $"{_currentLevel} / {_maxLevel}";
            _currentLevel++;
            _levelText.text = levelString;
        }

        private void OnRestartClicked()
        {
            _questionsPanel.SetActive(true);
            _gameEndPanel.gameObject.SetActive(false);
            StartGame();
        }
    }
}
