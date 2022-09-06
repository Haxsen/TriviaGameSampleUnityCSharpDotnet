using Haxsen.ScriptableObjects;
using UnityEngine;

namespace Haxsen.UI
{
    public class UIMainScreenManager : MonoBehaviour
    {
        [SerializeField] private OpenTdbOptionsSO openTdbOptionsSO;
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