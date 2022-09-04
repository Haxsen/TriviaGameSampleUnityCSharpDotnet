using Haxsen.Game;
using Haxsen.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Haxsen.UI
{
    public class UIAnswerSelfRecognizer : MonoBehaviour
    {
        [SerializeField] private UIQuestionManager uIQuestionManager;
        [SerializeField] private Image selfButtonImageReference;
        [SerializeField] private Button selfButtonReference;
        [SerializeField] private TextMeshProUGUI selfTextReference;

        [SerializeField] private UIColorOptionsSO uIColorOptionsSO;

        private void OnEnable()
        {
            UIQuestionManager.ActionPerformedOnQuestion += DisableInteraction;
        }

        private void OnDisable()
        {
            UIQuestionManager.ActionPerformedOnQuestion -= DisableInteraction;
        }

        public void EvaluateSelf()
        {
            bool isCorrect = gameObject.name.EndsWith("=correct");
            uIQuestionManager.EvaluateSelectedAnswer(isCorrect);
            SetSelfColors(isCorrect);
        }

        public void ShowAsCorrect() => SetSelfColors(true);

        private void SetSelfColors(bool isCorrect)
        {
            selfButtonImageReference.color = uIColorOptionsSO.GetButtonColor(isCorrect);
            selfTextReference.color = Color.white;
        }

        private void DisableInteraction()
        {
            selfButtonReference.interactable = false;
        }
    }
}