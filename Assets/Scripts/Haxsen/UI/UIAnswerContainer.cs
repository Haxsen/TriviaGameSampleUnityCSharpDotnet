using System.Web;
using TMPro;
using UnityEngine;

namespace Haxsen.UI
{
    public class UIAnswerContainer : MonoBehaviour
    {
        [SerializeField] private GameObject sampleAnswerObject;

        private GameObject _correctObject;

        private void Awake()
        {
            sampleAnswerObject.SetActive(false);
        }

        public void CreateAnswerObject(string description, bool isCorrect)
        {
            GameObject answerObject = Instantiate(sampleAnswerObject, transform);
            answerObject.GetComponentInChildren<TextMeshProUGUI>().text = HttpUtility.HtmlDecode(description);
            answerObject.SetActive(true);

            if (!isCorrect) return;
            answerObject.name = string.Concat(answerObject.name, "=correct");
            _correctObject = answerObject;
        }

        public void ClearAnswers()
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        public void ShowCorrectAnswer()
        {
            _correctObject.GetComponent<UIAnswerSelfRecognizer>().ShowAsCorrect();
        }
    }
}
