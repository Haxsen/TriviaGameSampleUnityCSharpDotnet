using Haxsen.DataStructures;
using Haxsen.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Haxsen.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameEventsSO gameEventsSO;
        [SerializeField] private GameSessionManager gameSessionManager;

        [Header("Events")]
        [SerializeField] private UnityEvent onGameEnd;

        private void OnEnable()
        {
            gameEventsSO.OnJsonReceived += StartGameWithJson;
            gameEventsSO.OnGameSessionCompleted += ShowGameEnd;
        }

        private void OnDisable()
        {
            gameEventsSO.OnJsonReceived -= StartGameWithJson;
            gameEventsSO.OnGameSessionCompleted -= ShowGameEnd;
        }

        private void StartGameWithJson(JsonResponseQuestionStructure jsonResponseQuestionStructure)
        {
            gameSessionManager.CreateGameSessionWithJson(jsonResponseQuestionStructure);
        }

        private void ShowGameEnd()
        {
            onGameEnd?.Invoke();
        }
    }
}