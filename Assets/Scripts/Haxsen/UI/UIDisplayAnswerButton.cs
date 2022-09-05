using Haxsen.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Haxsen.UI
{
    public class UIDisplayAnswerButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image buttonImage;
        [SerializeField] private TextMeshProUGUI textMesh;

        [SerializeField] private UIColorOptionsSO uIColorOptionsSO;
        [SerializeField] private UILabelOptionsSO uILabelOptionsSO;

        private Color _originalButtonImageColor;
        private string _countdownPrefix;

        private void Awake()
        {
            _originalButtonImageColor = buttonImage.color;
            
            ResetButton();
        }

        public void SetAnswerState(bool isCorrect)
        {
            button.interactable = false;
            buttonImage.color = uIColorOptionsSO.GetButtonColor(isCorrect);
            textMesh.color = Color.white;

            textMesh.text = isCorrect ? uILabelOptionsSO.correctAnswerLabel : uILabelOptionsSO.incorrectAnswerLabel;
        }

        public void AnswerDisplayed()
        {
            button.interactable = false;
            textMesh.text = uILabelOptionsSO.answerShownLabel;
        }

        public void SetCountdownPrefixToNext()
        {
            _countdownPrefix = uILabelOptionsSO.nextQuestionPrefixLabel;
        }

        public void SetCountdownPrefixToGameEnd()
        {
            _countdownPrefix = uILabelOptionsSO.gameEndPrefixLabel;
        }

        public void UpdateCountdown(int secondsRemaining)
        {
            textMesh.text = string.Concat(_countdownPrefix,secondsRemaining.ToString(), " ", uILabelOptionsSO.secondsLabel);
        }

        public void ResetColors()
        {
            buttonImage.color = _originalButtonImageColor;
            textMesh.color = Color.black;
        }

        public void ResetButton()
        {
            textMesh.text = uILabelOptionsSO.displayAnswerLabel;
            button.interactable = true;
        }
    }
}