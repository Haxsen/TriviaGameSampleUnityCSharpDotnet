using Haxsen.DataStructures;
using Haxsen.OpenTdb;
using Haxsen.ScriptableObjects;
using Haxsen.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Haxsen.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameEventsSO gameEventsSO;
        [SerializeField] private GameSessionManager gameSessionManager;
        [SerializeField] private UICategoryManager uICategoryManager;
        [SerializeField] private OpenTdbCommunication openTdbCommunication;

        [Header("Events")]
        [SerializeField] private UnityEvent onOpenTdbRequestStarted;
        [SerializeField] private UnityEvent onOpenTdbRequestResultSuccess;
        [SerializeField] private UnityEvent onOpenTdbRequestResultFail;
        [SerializeField] private UnityEvent onGameEnd;

        private void OnEnable()
        {
            gameEventsSO.OnJsonQuestionsReceived += StartGameWithJson;
            gameEventsSO.OnGameSessionCompleted += ShowGameEnd;
            gameEventsSO.OnJsonCategoriesReceived += UpdateCategoryScreen;

            gameEventsSO.OnOpenTdbRequestStarted += onOpenTdbRequestStarted.Invoke;
            gameEventsSO.OnOpenTdbRequestResultSuccess += onOpenTdbRequestResultSuccess.Invoke;
            gameEventsSO.OnOpenTdbRequestResultFail += onOpenTdbRequestResultFail.Invoke;
        }

        private void OnDisable()
        {
            gameEventsSO.OnJsonQuestionsReceived -= StartGameWithJson;
            gameEventsSO.OnGameSessionCompleted -= ShowGameEnd;
            gameEventsSO.OnJsonCategoriesReceived -= UpdateCategoryScreen;
        }

        public void StartGame()
        {
            openTdbCommunication.SendRequestQuestions();
        }

        public void FetchCategories()
        {
            openTdbCommunication.SendRequestCategories();
        }

        private void StartGameWithJson(JsonResponseQuestionStructure jsonResponseQuestionStructure)
        {
            gameSessionManager.CreateGameSessionWithJson(jsonResponseQuestionStructure);
        }

        private void UpdateCategoryScreen(JsonResponseCategoryStructure jsonResponseCategoryStructure)
        {
            uICategoryManager.UpdateCategoryList(jsonResponseCategoryStructure);
        }

        private void ShowGameEnd()
        {
            onGameEnd?.Invoke();
        }
    }
}