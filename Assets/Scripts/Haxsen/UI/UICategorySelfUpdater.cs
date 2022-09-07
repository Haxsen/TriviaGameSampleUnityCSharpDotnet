using Haxsen.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Haxsen.UI
{
    /// <summary>
    /// Self updater for selected category in ScriptableObject options.
    /// </summary>
    public class UICategorySelfUpdater : MonoBehaviour
    {
        [Header("ScriptableObject references")]
        [SerializeField] private OpenTdbOptionsSO openTdbOptionsSO;
        [SerializeField] private UILabelOptionsSO uILabelOptionsSO;
        
        [Header("Self component references")]
        [SerializeField] private TextMeshProUGUI textMesh;

        private void OnEnable()
        {
            textMesh.text = string.Format(uILabelOptionsSO.playSelectedCategoryPrefixLabel,
                openTdbOptionsSO.GetSelectedCategory().name);
        }
    }
}