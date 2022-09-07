using System.Linq;
using Haxsen.DataObjects;
using Haxsen.ScriptableObjects;
using UnityEngine;

namespace Haxsen.UI
{
    public class UICategoryManager : MonoBehaviour
    {
        [SerializeField] private OpenTdbOptionsSO openTdbOptionsSO;

        [SerializeField] private UICategoryContainer uICategoryContainer;

        public void UpdateCategoryList(JsonResponseCategoryStructure jsonResponseCategoryStructure)
        {
            uICategoryContainer.DisplayCategories(jsonResponseCategoryStructure.trivia_categories.ToList());
        }

        public void SetCategory(CategoryStructure category)
        {
            openTdbOptionsSO.SetCategory(category);
        }
    }
}