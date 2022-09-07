using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Haxsen.DataObjects;
using Haxsen.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Haxsen.UI
{
    /// <summary>
    /// Manages the questions on the UI.
    /// </summary>
    public class UIQuestionManager : MonoBehaviour
    {
        // When a user does an action during a question
        public UnityAction ActionPerformedOnQuestion;
        
        // When the game progresses to next question
        public UnityAction ProceedToNextQuestion;

        [Header("Functional component references")]
        [SerializeField] private GameSessionManager gameSessionManager;
        
        [SerializeField] private TextMeshProUGUI categoryLabel;
        [SerializeField] private TextMeshProUGUI questionNumberLabel;
        [SerializeField] private TextMeshProUGUI questionDescription;
        
        [SerializeField] private UIAnswerContainer answersContainer;
        [SerializeField] private UIDisplayAnswerButton displayAnswerButton;

        [Header("Values")]
        [SerializeField] private float postAnswerDelay = 1f;
        [SerializeField] private float countdownTillNext = 3f;

        private string _correctAnswer;

        /// <summary>
        /// Displays a question.
        /// </summary>
        /// <param name="questionStructure">The question model</param>
        /// <param name="number">The question number from the total</param>
        /// <param name="total">The total number of questions</param>
        public void DisplayQuestion(QuestionStructure questionStructure, int number, int total)
        {
            categoryLabel.text = questionStructure.category;
            questionNumberLabel.text = string.Concat("Question ", number, "/", total);
            questionDescription.text = HttpUtility.HtmlDecode(questionStructure.question);
            
            UpdateAnswerList(questionStructure);
        }

        /// <summary>
        /// Evaluates the selected answer.
        /// </summary>
        /// <param name="isCorrect">Whether the answer is correct</param>
        public void EvaluateSelectedAnswer(bool isCorrect)
        {
            ActionPerformedOnQuestion?.Invoke();
            
            displayAnswerButton.SetAnswerState(isCorrect);
            if (!isCorrect)
                answersContainer.ShowCorrectAnswer();
            
            StartCoroutine(NextQuestionState());
        }

        /// <summary>
        /// Displays the answer forced by the user.
        /// </summary>
        public void DisplayAnswer()
        {
            ActionPerformedOnQuestion?.Invoke();
            
            answersContainer.ShowCorrectAnswer();
            displayAnswerButton.OnAnswerDisplayed();

            StartCoroutine(NextQuestionState());
        }

        /// <summary>
        /// A coroutine that executes till the next question when current question is complete.
        /// </summary>
        /// <returns>Awaits the user till the next question is served</returns>
        private IEnumerator NextQuestionState()
        {
            yield return new WaitForSeconds(postAnswerDelay);
            displayAnswerButton.ResetColors();

            int nextQuestionNumber = gameSessionManager.GetCurrentQuestionNumber() + 1;
            if (nextQuestionNumber < gameSessionManager.GetTotalQuestions())
            {
                displayAnswerButton.SetCountdownPrefixToNext();
            }
            else
            {
                displayAnswerButton.SetCountdownPrefixToGameEnd();
            }
            
            float countdown = countdownTillNext;
            while (countdown >= 0)
            {
                displayAnswerButton.UpdateCountdown(countdown);
                yield return new WaitForSeconds(1);
                countdown--;
            }
            
            displayAnswerButton.ResetButton();
            ProceedToNextQuestion?.Invoke();
        }

        /// <summary>
        /// Randomizes and updates the answer list on the UI.
        /// </summary>
        /// <param name="questionStructure">The question model</param>
        private void UpdateAnswerList(QuestionStructure questionStructure)
        {
            List<string> answers = new List<string>();
            answers.Add(_correctAnswer = questionStructure.correct_answer);
            answers.AddRange(questionStructure.incorrect_answers);
            answers = RandomizeList(answers);
            DisplayAnswers(answers);
        }

        /// <summary>
        /// Displays the answer list on the UI.
        /// </summary>
        /// <param name="answers">The list of answers</param>
        private void DisplayAnswers(List<string> answers)
        {
            answersContainer.ClearAnswers();
            foreach (string answer in answers)
            {
                answersContainer.CreateAnswerObject(answer, string.Equals(answer, _correctAnswer));
            }
        }

        /// <summary>
        /// Randomizes a List by using Fisher Yates shuffle.
        /// </summary>
        /// <param name="list">The desired list to randomize</param>
        /// <typeparam name="T">The desired type of list</typeparam>
        /// <returns>List items in random order</returns>
        private List<T> RandomizeList<T>(List<T> list)
        {
            var rnd = new System.Random();
            return list.OrderBy(item => rnd.Next()).ToList();
        }
    }
}