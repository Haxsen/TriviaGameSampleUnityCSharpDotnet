using System;
using System.Collections.Generic;
using Haxsen.ScriptableObjects;
using UnityEngine;

namespace Haxsen.OpenTdb
{
    /// <summary>
    /// A concrete builder pattern to build URL for OpenTdb API.
    /// </summary>
    class OpenTdbUrlBuilder
    {
        private readonly string _baseUrl;
        private readonly List<string> _queriesUrl = new List<string>();

        public OpenTdbUrlBuilder(string startingQueriesUrl)
        {
            _baseUrl = startingQueriesUrl;
        }

        /// <summary>
        /// Adds amount of questions to the URL.
        /// </summary>
        /// <param name="amount">the amount of questions to request</param>
        /// <returns><c>OpenTdbUrlBuilder</c></returns>
        public OpenTdbUrlBuilder AddAmount(int amount)
        {
            if (IsAmountValid(amount))
                _queriesUrl.Add(string.Format(OpenTdbOptionsSO.OPENTDB_API_GET_AMOUNT, amount));
            
            return this;
        }

        /// <summary>
        /// Adds selection of category to the URL.
        /// </summary>
        /// <param name="categoryNumber">The category index</param>
        /// <returns><c>OpenTdbUrlBuilder</c></returns>
        public OpenTdbUrlBuilder SelectCategory(int categoryNumber)
        {
            if (IsCategoryValid(categoryNumber))
                _queriesUrl.Add(string.Format(OpenTdbOptionsSO.OPENTDB_API_GET_CATEGORY, categoryNumber));
            
            return this;
        }

        /// <summary>
        /// Builds the URL.
        /// </summary>
        /// <returns>the URL string</returns>
        public string Build()
        {
            string queriesCombined = String.Join("&", _queriesUrl);
            string finalUrl = String.Join("?", _baseUrl, queriesCombined);
            
            Debug.Log(string.Concat("Built OpenTdb URL: ", finalUrl));
            
            return finalUrl;
        }

        /// <summary>
        /// Checks if the category is valid and under range of OpenTdb API.
        /// </summary>
        /// <param name="categoryNumber">The category index</param>
        /// <returns>boolean whether category is valid</returns>
        public static bool IsCategoryValid(int categoryNumber)
        {
            return categoryNumber >= 9 && categoryNumber <= 32;
        }

        /// <summary>
        /// Checks if the amount is valid and above zero.
        /// </summary>
        /// <param name="amount">The amount to test</param>
        /// <returns>boolean whether amount is valid</returns>
        private static bool IsAmountValid(int amount)
        {
            return amount != 0;
        }
    }
}