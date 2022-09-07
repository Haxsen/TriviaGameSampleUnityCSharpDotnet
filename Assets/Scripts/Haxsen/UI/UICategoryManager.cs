using System.Linq;
using Haxsen.DataObjects;
using Haxsen.ScriptableObjects;
using UnityEngine;

namespace Haxsen.UI
{
    /// <summary>
    /// Manages the categories by fetching the data.
    /// </summary>
    public class UICategoryManager : MonoBehaviour
    {
        [Header("ScriptableObject references")]
        [SerializeField] private OpenTdbOptionsSO openTdbOptionsSO;

        [Header("Functional component references")]
        [SerializeField] private UICategoryContainer uICategoryContainer;

        /// <summary>
        /// Invokes the category container to update the categories with new category JSON.
        /// </summary>
        /// <param name="jsonResponseCategoryStructure">The new JSON for Categories</param>
        public void UpdateCategoryList(JsonResponseCategoryStructure jsonResponseCategoryStructure)
        {
            uICategoryContainer.DisplayCategories(jsonResponseCategoryStructure.trivia_categories.ToList());
        }

        /// <summary>
        /// Sets the category on the ScriptableObject.
        /// </summary>
        /// <param name="category">The new category</param>
        public void SetCategory(CategoryStructure category)
        {
            openTdbOptionsSO.SetCategory(category);
        }
    }
}