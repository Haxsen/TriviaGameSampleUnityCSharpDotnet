using System.Globalization;
using Haxsen.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Haxsen.UI
{
    /// <summary>
    /// Handles the button on the bottom of current question.
    /// </summary>
    public class UIDisplayAnswerButton : MonoBehaviour
    {
        [Header("ScriptableObject references")]
        [SerializeField] private UIColorOptionsSO uIColorOptionsSO;
        [SerializeField] private UILabelOptionsSO uILabelOptionsSO;
        
        [Header("Self component references")]
        [SerializeField] private Button button;
        [SerializeField] private Image buttonImage;
        [SerializeField] private TextMeshProUGUI textMesh;

        private Color _originalButtonImageColor;
        private string _countdownPrefix;

        private void Awake()
        {
            _originalButtonImageColor = buttonImage.color;
            
            ResetButton();
        }

        /// <summary>
        /// Sets the state when user has selected an answer.
        /// </summary>
        /// <param name="isCorrect">Whether the answer is correct</param>
        public void SetAnswerState(bool isCorrect)
        {
            button.interactable = false;
            buttonImage.color = uIColorOptionsSO.GetButtonColor(isCorrect);
            textMesh.color = Color.white;

            textMesh.text = isCorrect ? uILabelOptionsSO.correctAnswerLabel : uILabelOptionsSO.incorrectAnswerLabel;
        }

        /// <summary>
        /// Updates self when an answer is displayed, forced by user.
        /// </summary>
        public void OnAnswerDisplayed()
        {
            button.interactable = false;
            textMesh.text = uILabelOptionsSO.answerShownLabel;
        }

        /// <summary>
        /// Sets the countdown prefix to wait for next question.
        /// </summary>
        public void SetCountdownPrefixToNext()
        {
            _countdownPrefix = uILabelOptionsSO.nextQuestionPrefixLabel;
        }

        /// <summary>
        /// Sets the countdown prefix to wait for game end.
        /// </summary>
        public void SetCountdownPrefixToGameEnd()
        {
            _countdownPrefix = uILabelOptionsSO.gameEndPrefixLabel;
        }

        /// <summary>
        /// Updates the countdown after an answer.
        /// </summary>
        /// <param name="secondsRemaining">the remaining seconds till next question</param>
        public void UpdateCountdown(float secondsRemaining)
        {
            textMesh.text = string.Concat(_countdownPrefix, secondsRemaining.ToString(CultureInfo.InvariantCulture),
                " ", uILabelOptionsSO.secondsLabel);
        }

        /// <summary>
        /// Resets its color.
        /// </summary>
        public void ResetColors()
        {
            buttonImage.color = _originalButtonImageColor;
            textMesh.color = Color.black;
        }

        /// <summary>
        /// Resets self.
        /// </summary>
        public void ResetButton()
        {
            textMesh.text = uILabelOptionsSO.displayAnswerLabel;
            button.interactable = true;
        }
    }
}