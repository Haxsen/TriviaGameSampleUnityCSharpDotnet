using Haxsen.Game;
using Haxsen.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Haxsen.UI
{
    /// <summary>
    /// Self recognizing answer component.
    /// </summary>
    public class UIAnswerSelfRecognizer : MonoBehaviour
    {
        [Header("ScriptableObject references")]
        [SerializeField] private UIColorOptionsSO uIColorOptionsSO;
        
        [Header("Functional component references")]
        [SerializeField] private UIQuestionManager uIQuestionManager;
        
        [Header("Self component references")]
        [SerializeField] private Image selfButtonImageReference;
        [SerializeField] private Button selfButtonReference;
        [SerializeField] private TextMeshProUGUI selfTextReference;
        
        private void OnEnable()
        {
            uIQuestionManager.ActionPerformedOnQuestion += DisableInteraction;
        }

        private void OnDisable()
        {
            uIQuestionManager.ActionPerformedOnQuestion -= DisableInteraction;
        }

        /// <summary>
        /// Force show this answer as correct.
        /// </summary>
        public void ShowAsCorrect() => SetSelfColors(true);

        /// <summary>
        /// Self evaluation by checking its gameObject name.
        /// </summary>
        public void EvaluateSelf()
        {
            bool isCorrect = gameObject.name.EndsWith("=correct");
            uIQuestionManager.EvaluateSelectedAnswer(isCorrect);
            SetSelfColors(isCorrect);
        }

        /// <summary>
        /// Update self colors using <c>UIColorOptionsSO</c>.
        /// </summary>
        /// <param name="isCorrect"></param>
        private void SetSelfColors(bool isCorrect)
        {
            selfButtonImageReference.color = uIColorOptionsSO.GetButtonColor(isCorrect);
            selfTextReference.color = Color.white;
        }

        /// <summary>
        /// Disables self button's interaction.
        /// </summary>
        private void DisableInteraction()
        {
            selfButtonReference.interactable = false;
        }
    }
}