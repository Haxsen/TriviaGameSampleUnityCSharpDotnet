using Haxsen.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Haxsen.UI
{
    public class UICategorySelfUpdater : MonoBehaviour
    {
        [SerializeField] private OpenTdbOptionsSO openTdbOptionsSO;
        [SerializeField] private UILabelOptionsSO uILabelOptionsSO;
        [SerializeField] private TextMeshProUGUI textMesh;

        private void OnEnable()
        {
            textMesh.text = string.Format(uILabelOptionsSO.playSelectedCategoryPrefixLabel,
                openTdbOptionsSO.GetSelectedCategory().name);
        }
    }
}