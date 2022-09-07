using System.Collections.Generic;
using Haxsen.DataObjects;
using UnityEngine;

namespace Haxsen.UI
{
    /// <summary>
    /// Handles the category list shown on UI.
    /// </summary>
    public class UICategoryContainer : MonoBehaviour
    {
        [SerializeField] private GameObject sampleCategoryButton;
        
        private void Awake()
        {
            sampleCategoryButton.SetActive(false);
        }
        
        /// <summary>
        /// Displays the categories by fetching the new category list.
        /// </summary>
        /// <param name="categories">The new category list</param>
        public void DisplayCategories(List<CategoryStructure> categories)
        {
            ClearCategories();
            foreach (CategoryStructure category in categories)
            {
                CreateCategoryButton(category);
            }
        }

        /// <summary>
        /// Creates a category button for selection.
        /// </summary>
        /// <param name="category">The assigned category</param>
        public void CreateCategoryButton(CategoryStructure category)
        {
            GameObject categoryButton = Instantiate(sampleCategoryButton, transform);
            UISelectCategoryButton categoryButtonComponent = categoryButton.GetComponent<UISelectCategoryButton>();
            
            categoryButtonComponent.Category = category;
            categoryButtonComponent.SetLabel(category.name);
            
            categoryButton.SetActive(true);
        }

        /// <summary>
        /// Resets the category list.
        /// </summary>
        public void ClearCategories()
        {
            Helper.DestroyChildrenOfList(transform, 1);
        }
    }
}