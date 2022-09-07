using System.Collections.Generic;
using Haxsen.DataObjects;
using Haxsen.ScriptableObjects;
using Haxsen.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Haxsen.Game
{
    /// <summary>
    /// Manages a game session and flow of the game with the fetched question & answer data.
    /// </summary>
    public class GameSessionManager : MonoBehaviour
    {
        [Header("ScriptableObject references")]
        [SerializeField] private GameEventsSO gameEventsSO;
        
        [Header("Functional component references")]
        [SerializeField] private UIQuestionManager uIQuestionManager;
    
        // Persistently store questions list of current session
        private List<QuestionStructure> _questionList;
        
        // Index of current question
        private int _currentQuestion = -1;

        private void OnEnable()
        {
            uIQuestionManager.ProceedToNextQuestion += DisplayNextQuestion;
        }

        private void OnDisable()
        {
            uIQuestionManager.ProceedToNextQuestion -= DisplayNextQuestion;
        }

        public int GetTotalQuestions() => _questionList.Count;
        public int GetCurrentQuestionNumber() => _currentQuestion;

        /// <summary>
        /// Creates a game session by resetting and using new questions list.
        /// </summary>
        /// <param name="jsonResponseQuestionStructure">The full JSON containing question & answer list</param>
        public void CreateGameSessionWithJson(JsonResponseQuestionStructure jsonResponseQuestionStructure)
        {
            ResetGameSession();
            _questionList = jsonResponseQuestionStructure.results;
            DisplayNextQuestion();
        }

        /// <summary>
        /// Proceeds to the next question. Starts with first if current question index is -1.
        /// </summary>
        private void DisplayNextQuestion()
        {
            _currentQuestion++;

            if (_currentQuestion >= GetTotalQuestions())
            {
                FinishSession();
                return;
            }
            
            uIQuestionManager.DisplayQuestion(
                _questionList[_currentQuestion],
                _currentQuestion+1,
                GetTotalQuestions());
        }

        /// <summary>
        /// Resets the current session.
        /// </summary>
        private void ResetGameSession()
        {
            _currentQuestion = -1;
        }

        /// <summary>
        /// Finishes the current session.
        /// </summary>
        private void FinishSession()
        {
            gameEventsSO.OnGameSessionCompleted?.Invoke();
        }
    }
}
