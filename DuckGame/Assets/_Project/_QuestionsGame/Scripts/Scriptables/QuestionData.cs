using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuizGame
{
    [CreateAssetMenu(fileName = "New Question", menuName = "ScriptableObjects/Question")]
    public class QuestionData : ScriptableObject
    {
        public string Question;
        public AnswerData[] Answers;
    }

    [Serializable]
    public class AnswerData
    {
        public string AnswerText;
        public AnswerType AnswerType;
    }

    public enum AnswerType
    {
        Incorrect,
        Correct
    }
}
