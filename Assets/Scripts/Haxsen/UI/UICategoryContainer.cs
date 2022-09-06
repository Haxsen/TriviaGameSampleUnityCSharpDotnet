using System.Collections.Generic;
using Haxsen.DataStructures;
using UnityEngine;

namespace Haxsen.UI
{
    public class UICategoryContainer : MonoBehaviour
    {
        [SerializeField] private GameObject sampleCategoryButton;
        
        private void Awake()
        {
            sampleCategoryButton.SetActive(false);
        }
        
        public void DisplayCategories(List<CategoryStructure> categories)
        {
            ClearCategories();
            foreach (CategoryStructure category in categories)
            {
                CreateCategoryButton(category);
            }
        }

        public void CreateCategoryButton(CategoryStructure category)
        {
            GameObject categoryButton = Instantiate(sampleCategoryButton, transform);
            UISelectCategoryButton categoryButtonComponent = categoryButton.GetComponent<UISelectCategoryButton>();
            categoryButtonComponent.Category = category;
            categoryButtonComponent.SetLabel(category.name);
            categoryButton.SetActive(true);
        }

        public void ClearCategories()
        {
            Helper.DestroyChildrenOfList(transform, 1);
        }
    }
}