using System.Collections;
using Haxsen.DataObjects;
using Haxsen.ScriptableObjects;
using Haxsen.Singleton;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Haxsen.OpenTdb
{
    /// <summary>
    /// Handles the OpenTdb API communication, e.g. Receiving data in JSON format.
    /// </summary>
    public class OpenTdbCommunication : PersistentSingleton<OpenTdbCommunication>
    {
        [Header("ScriptableObject references")]
        [SerializeField] private OpenTdbOptionsSO openTdbOptionsSO;
        [SerializeField] private GameEventsSO gameEventsSO;

        /// <summary>
        /// Initiates a request to OpenTdb for questions.
        /// </summary>
        public void SendRequestQuestions()
        {
            GetOpenTdbQuestionsJson(JsonQuestionsSuccess, OnRequestFail);
        }

        /// <summary>
        /// Initiates a request to OpenTdb for categories.
        /// </summary>
        public void SendRequestCategories()
        {
            GetOpenTdbCategoriesJson(JsonCategoriesSuccess, OnRequestFail);
        }

        /// <summary>
        /// Callback when the question list JSON is received.
        /// </summary>
        /// <param name="json">The new JSON for Questions</param>
        private void JsonQuestionsSuccess(JsonResponseQuestionStructure json)
        {
            Debug.Log($"Fetched JSON for questions:\n{json}");
            gameEventsSO.OnJsonQuestionsReceived.Invoke(json);
        }

        /// <summary>
        /// Callback when the category list JSON is received.
        /// </summary>
        /// <param name="json">The new JSON for Categories</param>
        private void JsonCategoriesSuccess(JsonResponseCategoryStructure json)
        {
            Debug.Log($"Fetched JSON for categories:\n{json}");
            gameEventsSO.OnJsonCategoriesReceived.Invoke(json);
        }

        /// <summary>
        /// Callback when the request fails.
        /// </summary>
        /// <param name="message">The error message.</param>
        private void OnRequestFail(string message)
        {
            Debug.LogError($"Request to fetch JSON from OpenTdb was failed. Details are: \n{message}");
            gameEventsSO.OnOpenTdbRequestResultFail?.Invoke();
        }
        
        /// <summary>
        /// Calls the server API to fetch questions.
        /// </summary>
        /// <param name="callbackOnSuccess">Callback on success.</param>
        /// <param name="callbackOnFail">Callback on fail.</param>
        private void GetOpenTdbQuestionsJson(UnityAction<JsonResponseQuestionStructure> callbackOnSuccess, UnityAction<string> callbackOnFail)
        {
            SendRequest(openTdbOptionsSO.GetFullUrl(), callbackOnSuccess, callbackOnFail);
        }
        
        /// <summary>
        /// Calls the server API to fetch categories.
        /// </summary>
        /// <param name="callbackOnSuccess">Callback on success.</param>
        /// <param name="callbackOnFail">Callback on fail.</param>
        private void GetOpenTdbCategoriesJson(UnityAction<JsonResponseCategoryStructure> callbackOnSuccess, UnityAction<string> callbackOnFail)
        {
            SendRequest(OpenTdbOptionsSO.OPENTDB_API_CATEGORY_FORMAT, callbackOnSuccess, callbackOnFail);
        }

        /// <summary>
        /// This method is used to begin sending request process.
        /// </summary>
        /// <param name="url">API url.</param>
        /// <param name="callbackOnSuccess">Callback on success.</param>
        /// <param name="callbackOnFail">Callback on fail.</param>
        /// <typeparam name="T">Data Model Type.</typeparam>
        private void SendRequest<T>(string url, UnityAction<T> callbackOnSuccess, UnityAction<string> callbackOnFail)
        {
            gameEventsSO.OnOpenTdbRequestStarted?.Invoke();
            StartCoroutine(RequestCoroutine(url, callbackOnSuccess, callbackOnFail));
        }
        
        /// <summary>
        /// Coroutine that handles communication with REST server.
        /// </summary>
        /// <returns>The coroutine.</returns>
        /// <param name="url">API url.</param>
        /// <param name="callbackOnSuccess">Callback on success.</param>
        /// <param name="callbackOnFail">Callback on fail.</param>
        /// <typeparam name="T">Data Model Type.</typeparam>
        private IEnumerator RequestCoroutine<T>(string url, UnityAction<T> callbackOnSuccess, UnityAction<string> callbackOnFail)
        {
            var unityWebRequest = UnityWebRequest.Get(url);
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError
                || unityWebRequest.result == UnityWebRequest.Result.ProtocolError
                || unityWebRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError(unityWebRequest.error);
                callbackOnFail?.Invoke(unityWebRequest.error);
            }
            else
            {
                Debug.Log(unityWebRequest.downloadHandler.text);
                ParseResponse(unityWebRequest.downloadHandler.text, callbackOnSuccess);
            }
        }
        
        /// <summary>
        /// This method finishes request process as we have received answer from server.
        /// </summary>
        /// <param name="data">Data received from server in JSON format.</param>
        /// <param name="callbackOnSuccess">Callback on success.</param>
        /// <typeparam name="T">Data Model Type.</typeparam>
        private void ParseResponse<T>(string data, UnityAction<T> callbackOnSuccess)
        {
            var parsedData = JsonUtility.FromJson<T>(data);
            callbackOnSuccess?.Invoke(parsedData);
            gameEventsSO.OnOpenTdbRequestResultSuccess?.Invoke();
        }
    }
}