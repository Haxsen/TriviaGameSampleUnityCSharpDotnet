using Haxsen.DataObjects;
using Haxsen.OpenTdb;
using Haxsen.ScriptableObjects;
using Haxsen.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Haxsen.Game
{
    /// <summary>
    /// Manages the flow of the game.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("ScriptableObject references")]
        [SerializeField] private GameEventsSO gameEventsSO;
        
        [Header("Functional component references")]
        [SerializeField] private GameSessionManager gameSessionManager;
        [SerializeField] private UICategoryManager uICategoryManager;

        [Header("Events")]
        [SerializeField] private UnityEvent onOpenTdbRequestStarted;
        [SerializeField] private UnityEvent onOpenTdbRequestResultSuccess;
        [SerializeField] private UnityEvent onOpenTdbRequestResultFail;
        [SerializeField] private UnityEvent onGameEnd;

        private void OnEnable()
        {
            // Game events
            gameEventsSO.OnJsonQuestionsReceived += StartGameWithJson;
            gameEventsSO.OnJsonCategoriesReceived += UpdateCategoryScreen;
            gameEventsSO.OnGameSessionCompleted += ShowGameEnd;

            // OpenTdb events
            gameEventsSO.OnOpenTdbRequestStarted += onOpenTdbRequestStarted.Invoke;
            gameEventsSO.OnOpenTdbRequestResultSuccess += onOpenTdbRequestResultSuccess.Invoke;
            gameEventsSO.OnOpenTdbRequestResultFail += onOpenTdbRequestResultFail.Invoke;
        }

        private void OnDisable()
        {
            gameEventsSO.OnJsonQuestionsReceived -= StartGameWithJson;
            gameEventsSO.OnJsonCategoriesReceived -= UpdateCategoryScreen;
            gameEventsSO.OnGameSessionCompleted -= ShowGameEnd;
            
            gameEventsSO.OnOpenTdbRequestStarted -= onOpenTdbRequestStarted.Invoke;
            gameEventsSO.OnOpenTdbRequestResultSuccess -= onOpenTdbRequestResultSuccess.Invoke;
            gameEventsSO.OnOpenTdbRequestResultFail -= onOpenTdbRequestResultFail.Invoke;
        }

        /// <summary>
        /// Starts the game by sending request for fetching questions to OpenTdb server.
        /// </summary>
        public void StartGame()
        {
            OpenTdbCommunication.Instance.SendRequestQuestions();
        }

        /// <summary>
        /// Fetches the category list from OpenTdb server.
        /// </summary>
        public void FetchCategories()
        {
            OpenTdbCommunication.Instance.SendRequestCategories();
        }

        /// <summary>
        /// Starts the game by creating a session using the provided Question & Answer data.
        /// </summary>
        /// <param name="jsonResponseQuestionStructure">The full JSON containing question & answer list</param>
        private void StartGameWithJson(JsonResponseQuestionStructure jsonResponseQuestionStructure)
        {
            gameSessionManager.CreateGameSessionWithJson(jsonResponseQuestionStructure);
        }

        /// <summary>
        /// Sends the request to update the category screen on UI.
        /// </summary>
        /// <param name="jsonResponseCategoryStructure">The full JSON containing all categories</param>
        private void UpdateCategoryScreen(JsonResponseCategoryStructure jsonResponseCategoryStructure)
        {
            uICategoryManager.UpdateCategoryList(jsonResponseCategoryStructure);
        }

        /// <summary>
        /// Triggers the end of game.
        /// </summary>
        private void ShowGameEnd()
        {
            onGameEnd?.Invoke();
        }
    }
}