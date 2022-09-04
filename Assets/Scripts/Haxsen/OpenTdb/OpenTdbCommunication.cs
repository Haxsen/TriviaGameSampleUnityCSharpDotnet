using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using Haxsen.DataStructures;
using Haxsen.ScriptableObjects;
using Haxsen.Singleton;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Haxsen.OpenTdb
{
    public class OpenTdbCommunication : PersistentSingleton<OpenTdbCommunication>
    {
        [SerializeField] private OpenTdbOptionsSO openTdbOptionsSO;
        [SerializeField] private GameEventsSO gameEventsSO;

        public void SendReq()
        {
            GetOpenTdbJson(Success, Fail);
        }

        private void Success(JsonResponseStructure json)
        {
            Debug.Log(json.ToString());
            gameEventsSO.OnJsonReceived.Invoke(json);
        }

        private void Fail(string message)
        {
            Debug.LogError(message);
        }
        
        /// <summary>
        /// This method call server API to get a quote.
        /// </summary>
        /// <param name="callbackOnSuccess">Callback on success.</param>
        /// <param name="callbackOnFail">Callback on fail.</param>
        public void GetOpenTdbJson(UnityAction<JsonResponseStructure> callbackOnSuccess, UnityAction<string> callbackOnFail)
        {
            SendRequest(openTdbOptionsSO.GetUrl(), callbackOnSuccess, callbackOnFail);
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
            var www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError
                || www.result == UnityWebRequest.Result.ProtocolError
                || www.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError(www.error);
                callbackOnFail?.Invoke(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                ParseResponse(www.downloadHandler.text, callbackOnSuccess, callbackOnFail);
            }
        }
        
        /// <summary>
        /// This method finishes request process as we have received answer from server.
        /// </summary>
        /// <param name="data">Data received from server in JSON format.</param>
        /// <param name="callbackOnSuccess">Callback on success.</param>
        /// <param name="callbackOnFail">Callback on fail.</param>
        /// <typeparam name="T">Data Model Type.</typeparam>
        private void ParseResponse<T>(string data, UnityAction<T> callbackOnSuccess, UnityAction<string> callbackOnFail)
        {
            // data = data.Replace("&quote;", "\\\"");
            // data = HttpUtility.HtmlDecode(data);
            // Debug.Log(data);
            var parsedData = JsonUtility.FromJson<T>(data);
            callbackOnSuccess?.Invoke(parsedData);
        }
    }
}