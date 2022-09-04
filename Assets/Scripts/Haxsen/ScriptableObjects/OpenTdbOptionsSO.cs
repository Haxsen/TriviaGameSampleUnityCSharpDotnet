using Haxsen.OpenTdb;
using UnityEngine;

namespace Haxsen.ScriptableObjects
{
    [CreateAssetMenu(fileName = "OpenTdbOptionsSO", menuName = "ScriptableObjects/OpenTdbOptions", order = 1)]
    public class OpenTdbOptionsSO : ScriptableObject
    {
        [SerializeField] private int numberOfQuestionsToFetch = 10;
        [SerializeField] private int selectedCategory;
        
        private string _url;
        
        public string GetUrl()
        {
            BuildUrl();
            return _url;
        }
        
        public void SetAmount(int amount)
        {
            numberOfQuestionsToFetch = amount;
        }
        
        public void SetCategory(int categoryNumber)
        {
            selectedCategory = categoryNumber;
        }
        
        private void BuildUrl()
        {
            Debug.Log("Building URL");
            _url = new OpenTdbUrlBuilder(OpenTdbConfig.OPENTDB_API_URL_FORMAT)
                .AddAmount(numberOfQuestionsToFetch)
                .SelectCategory(selectedCategory)
                .Build();
        }
    }
}