using Haxsen.ScriptableObjects;
using UnityEngine;

namespace Haxsen.UI
{
    /// <summary>
    /// Manages the main screen on the UI.
    /// </summary>
    public class UIMainScreenManager : MonoBehaviour
    {
        [Header("ScriptableObject references")]
        [SerializeField] private OpenTdbOptionsSO openTdbOptionsSO;
        
        [Header("Functional component references")]
        [SerializeField] private GameObject previousCategoryButton;

        private void OnEnable()
        {
            if (openTdbOptionsSO.IsSelectedCategoryValid())
            {
                previousCategoryButton.SetActive(true);
            }
            else
            {
                previousCategoryButton.SetActive(false);
            }
        }
    }
}