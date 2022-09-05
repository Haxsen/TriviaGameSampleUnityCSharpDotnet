using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Haxsen.DataStructures;
using Haxsen.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Haxsen.Game
{
    public class UIQuestionManager : MonoBehaviour
    {
        public static UnityAction ActionPerformedOnQuestion;

        [SerializeField] private GameSessionManager gameSessionManager;
        
        [SerializeField] private TextMeshProUGUI categoryLabel;
        [SerializeField] private TextMeshProUGUI questionNumberLabel;
        [SerializeField] private TextMeshProUGUI questionDescription;
        [SerializeField] private UIAnswerContainer answersContainer;
        [SerializeField] private UIDisplayAnswerButton displayAnswerButton;

        private string _correctAnswer;

        public void DisplayQuestions(QuestionStructure questionStructure, int number, int total)
        {
            categoryLabel.text = questionStructure.category;
            questionNumberLabel.text = string.Concat("Question ", number, "/", total);
            questionDescription.text = HttpUtility.HtmlDecode(questionStructure.question);
            
            UpdateAnswerList(questionStructure);
        }

        public void EvaluateSelectedAnswer(bool isCorrect)
        {
            ActionPerformedOnQuestion?.Invoke();
            
            displayAnswerButton.SetAnswerState(isCorrect);
            if (!isCorrect)
                answersContainer.ShowCorrectAnswer();
            
            StartCoroutine(NextQuestionState());
        }

        public void DisplayAnswer()
        {
            ActionPerformedOnQuestion?.Invoke();
            
            answersContainer.ShowCorrectAnswer();
            displayAnswerButton.AnswerDisplayed();

            StartCoroutine(NextQuestionState());
        }

        private IEnumerator NextQuestionState()
        {
            yield return new WaitForSeconds(1);
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
            
            int countdown = 3;
            while (countdown >= 0)
            {
                displayAnswerButton.UpdateCountdown(countdown);
                yield return new WaitForSeconds(1);
                countdown--;
            }
            
            displayAnswerButton.ResetButton();
            GameSessionManager.ProceedToNextQuestion?.Invoke();
        }

        private void UpdateAnswerList(QuestionStructure questionStructure)
        {
            List<string> answers = new List<string>();
            answers.Add(_correctAnswer = questionStructure.correct_answer);
            answers.AddRange(questionStructure.incorrect_answers);
            answers = RandomizeList(answers);
            DisplayAnswers(answers);
        }

        private void DisplayAnswers(List<string> answers)
        {
            answersContainer.ClearAnswers();
            foreach (string answer in answers)
            {
                answersContainer.CreateAnswerObject(answer, string.Equals(answer, _correctAnswer));
            }
        }

        private List<T> RandomizeList<T>(List<T> list)
        {
            var rnd = new System.Random();
            return list.OrderBy(item => rnd.Next()).ToList();
        }
    }
}