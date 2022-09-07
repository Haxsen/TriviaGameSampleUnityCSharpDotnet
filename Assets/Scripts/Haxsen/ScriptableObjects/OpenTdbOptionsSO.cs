using Haxsen.DataObjects;
using Haxsen.OpenTdb;
using UnityEngine;

namespace Haxsen.ScriptableObjects
{
    /// <summary>
    /// A ScriptableObject containing options for OpenTdb API.
    /// </summary>
    [CreateAssetMenu(fileName = "OpenTdbOptionsSO", menuName = "ScriptableObjects/OpenTdbOptions", order = 1)]
    public class OpenTdbOptionsSO : ScriptableObject
    {
        private const string OPENTDB_API_URL_FORMAT = "https://opentdb.com/api.php";
        
        public const string OPENTDB_API_CATEGORY_FORMAT = "https://opentdb.com/api_category.php";
        public const string OPENTDB_API_GET_AMOUNT = "amount={0}";
        public const string OPENTDB_API_GET_CATEGORY = "category={0}";
        
        [SerializeField] private int numberOfQuestionsToFetch = 10;
        [SerializeField] private CategoryStructure selectedCategory;

        public bool IsSelectedCategoryValid() => OpenTdbUrlBuilder.IsCategoryValid(selectedCategory.id);
        public void ResetCategories() => selectedCategory.id = 0;
        
        /// <summary>
        /// Builds and returns the full URL.
        /// </summary>
        /// <returns>The built URL for OpenTdb API</returns>
        public string GetFullUrl()
        {
            return BuildUrl();
        }

        /// <summary>
        /// Returns the current CategoryStructure object.
        /// </summary>
        /// <returns>The selected category object</returns>
        public CategoryStructure GetSelectedCategory() => selectedCategory;
        
        /// <summary>
        /// Sets the questions list amount.
        /// </summary>
        /// <param name="amount">The amount of questions that will be used to fetch</param>
        public void SetAmount(int amount)
        {
            numberOfQuestionsToFetch = amount;
        }
        
        /// <summary>
        /// Sets the category.
        /// </summary>
        /// <param name="category">The preferred category</param>
        public void SetCategory(CategoryStructure category)
        {
            selectedCategory = category;
        }
        
        /// <summary>
        /// Builds the full URL.
        /// </summary>
        /// <returns>The built URL</returns>
        private string BuildUrl()
        {
            return new OpenTdbUrlBuilder(OPENTDB_API_URL_FORMAT)
                .AddAmount(numberOfQuestionsToFetch)
                .SelectCategory(selectedCategory.id)
                .Build();
        }
    }
}
