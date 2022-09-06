using System;
using System.Collections.Generic;
using Haxsen.OpenTdb;
using UnityEngine;

namespace Haxsen.ScriptableObjects
{
    class OpenTdbUrlBuilder
    {
        private string _baseUrl;
        private List<string> _queriesUrl = new List<string>();

        public OpenTdbUrlBuilder(string startingQueriesUrl)
        {
            _baseUrl = startingQueriesUrl;
        }

        public OpenTdbUrlBuilder AddAmount(int amount)
        {
            if (isAmountValid(amount))
                _queriesUrl.Add(string.Format(OpenTdbConfig.OPENTDB_API_GET_AMOUNT, amount));
            return this;
        }

        public OpenTdbUrlBuilder SelectCategory(int categoryNumber)
        {
            if (isCategoryValid(categoryNumber))
                _queriesUrl.Add(string.Format(OpenTdbConfig.OPENTDB_API_GET_CATEGORY, categoryNumber));
            return this;
        }

        public string Build()
        {
            string queriesCombined = String.Join("&", _queriesUrl);
            string finalUrl = String.Join("?", _baseUrl, queriesCombined);
            Debug.Log(string.Concat("Built OpenTdb URL: ", finalUrl));
            return finalUrl;
        }

        public static bool isCategoryValid(int categoryNumber)
        {
            return categoryNumber >= 9 && categoryNumber <= 32;
        }

        private bool isAmountValid(int amount)
        {
            return amount != 0;
        }
    }
}