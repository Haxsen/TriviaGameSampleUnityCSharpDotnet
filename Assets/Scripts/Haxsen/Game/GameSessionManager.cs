using System.Collections.Generic;
using Haxsen.DataStructures;
using UnityEngine;
using UnityEngine.Events;

namespace Haxsen.Game
{
    public class GameSessionManager : MonoBehaviour
    {
        public static UnityAction ProceedToNextQuestion;
        
        [SerializeField] private UIQuestionManager uIQuestionManager;
    
        private List<QuestionStructure> _questionList;
        private int _currentQuestion = -1;

        private void OnEnable()
        {
            ProceedToNextQuestion += DisplayNextQuestion;
        }

        private void OnDisable()
        {
            ProceedToNextQuestion -= DisplayNextQuestion;
        }

        public void CreateGameSessionWithJson(JsonResponseStructure jsonResponseStructure)
        {
            ResetGameSession();
            _questionList = jsonResponseStructure.results;
            DisplayNextQuestion();
        }

        private void DisplayNextQuestion()
        {
            _currentQuestion++;

            int totalQuestions = _questionList.Count;
            if (_currentQuestion >= totalQuestions)
                return;
            
            uIQuestionManager.DisplayQuestion(
                _questionList[_currentQuestion],
                _currentQuestion+1,
                totalQuestions);
        }

        private void ResetGameSession()
        {
            _currentQuestion = -1;
        }
    }
}
