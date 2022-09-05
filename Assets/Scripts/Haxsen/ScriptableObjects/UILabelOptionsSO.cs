﻿using UnityEngine;

namespace Haxsen.ScriptableObjects
{
    [CreateAssetMenu(fileName = "UILabelOptionsSO", menuName = "ScriptableObjects/UILabelOptions", order = 1)]
    public class UILabelOptionsSO : ScriptableObject
    {
        public string correctAnswerLabel = "Correct";
        public string incorrectAnswerLabel = "Wrong!";
        public string answerShownLabel = "Answer is shown.";
        public string nextQuestionPrefixLabel = "Next Question in ";
        public string gameEndPrefixLabel = "Game ending in ";
        public string secondsLabel = "seconds";
        public string displayAnswerLabel = "Display Answer";
    }
}