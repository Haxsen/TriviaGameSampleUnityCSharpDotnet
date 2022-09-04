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

        private Color _originalButtonImageColor;

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

            textMesh.text = isCorrect ? "Correct" : "Wrong!";
        }

        public void AnswerDisplayed()
        {
            button.interactable = false;
            textMesh.text = "Answer is shown in Green";
        }

        public void UpdateCountdown(int secondsRemaining)
        {
            textMesh.text = string.Concat("Next Question in ",secondsRemaining.ToString(), " seconds");
        }

        public void ResetColors()
        {
            buttonImage.color = _originalButtonImageColor;
            textMesh.color = Color.black;
        }

        public void ResetButton()
        {
            textMesh.text = "Display Answer";
            button.interactable = true;
        }
    }
}