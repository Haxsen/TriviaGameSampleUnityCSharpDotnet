using System.Collections.Generic;
using Haxsen.DataStructures;
using Haxsen.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Haxsen.Game
{
    public class GameSessionManager : MonoBehaviour
    {
        public static UnityAction ProceedToNextQuestion;
        
        [SerializeField] private UIQuestionManager uIQuestionManager;
        [SerializeField] private GameEventsSO gameEventsSO;
    
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

            if (_currentQuestion >= GetTotalQuestions())
            {
                FinishSession();
                return;
            }
            
            uIQuestionManager.DisplayQuestions(
                _questionList[_currentQuestion],
                _currentQuestion+1,
                GetTotalQuestions());
        }

        private void ResetGameSession()
        {
            _currentQuestion = -1;
        }

        private void FinishSession()
        {
            gameEventsSO.OnGameSessionCompleted?.Invoke();
        }

        public int GetTotalQuestions() => _questionList.Count;
        public int GetCurrentQuestionNumber() => _currentQuestion;
    }
}
