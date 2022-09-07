using Haxsen.DataObjects;
using Haxsen.OpenTdb;
using UnityEngine;

namespace Haxsen.ScriptableObjects
{
    [CreateAssetMenu(fileName = "OpenTdbOptionsSO", menuName = "ScriptableObjects/OpenTdbOptions", order = 1)]
    public class OpenTdbOptionsSO : ScriptableObject
    {
        [SerializeField] private int numberOfQuestionsToFetch = 10;
        [SerializeField] private CategoryStructure selectedCategory;
        [SerializeField] private string categoriesUrl;
        
        private string _url;
        
        public string GetUrl()
        {
            BuildUrl();
            return _url;
        }

        public string GetCategoriesUrl() => categoriesUrl;

        public CategoryStructure GetSelectedCategory() => selectedCategory;

        public bool IsSelectedCategoryValid() => OpenTdbUrlBuilder.isCategoryValid(selectedCategory.id);
        
        public void SetAmount(int amount)
        {
            numberOfQuestionsToFetch = amount;
        }
        
        public void SetCategory(CategoryStructure category)
        {
            selectedCategory = category;
        }

        public void ResetCategories() => selectedCategory.id = 0;
        
        private void BuildUrl()
        {
            Debug.Log("Building URL");
            _url = new OpenTdbUrlBuilder(OpenTdbConfig.OPENTDB_API_URL_FORMAT)
                .AddAmount(numberOfQuestionsToFetch)
                .SelectCategory(selectedCategory.id)
                .Build();
        }
    }
}
