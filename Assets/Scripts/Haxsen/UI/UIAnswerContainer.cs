using System.Web;
using TMPro;
using UnityEngine;

namespace Haxsen.UI
{
    /// <summary>
    /// Manages the answer list shown on UI.
    /// </summary>
    public class UIAnswerContainer : MonoBehaviour
    {
        [SerializeField] private GameObject sampleAnswerObject;

        private GameObject _correctObject;

        private void Awake()
        {
            sampleAnswerObject.SetActive(false);
        }

        /// <summary>
        /// Creates a button for an answer.
        /// </summary>
        /// <param name="description">The description of answer</param>
        /// <param name="isCorrect">Whether this answer is correct</param>
        public void CreateAnswerObject(string description, bool isCorrect)
        {
            GameObject answerObject = Instantiate(sampleAnswerObject, transform);
            answerObject.GetComponentInChildren<TextMeshProUGUI>().text = HttpUtility.HtmlDecode(description);
            answerObject.SetActive(true);

            if (!isCorrect) return;
            answerObject.name = string.Concat(answerObject.name, "=correct");
            _correctObject = answerObject;
        }

        /// <summary>
        /// Resets the answer list.
        /// </summary>
        public void ClearAnswers()
        {
            Helper.DestroyChildrenOfList(transform, 1);
        }

        /// <summary>
        /// Shows the correct answer.
        /// </summary>
        public void ShowCorrectAnswer()
        {
            _correctObject.GetComponent<UIAnswerSelfRecognizer>().ShowAsCorrect();
        }
    }
}
