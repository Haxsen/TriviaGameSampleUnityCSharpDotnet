using Haxsen.DataStructures;
using Haxsen.ScriptableObjects;
using UnityEngine;

namespace Haxsen.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameEventsSO gameEventsSO;
        [SerializeField] private GameSessionManager gameSessionManager;

        private void OnEnable()
        {
            gameEventsSO.OnJsonReceived += StartGameWithJson;
        }

        private void OnDisable()
        {
            gameEventsSO.OnJsonReceived -= StartGameWithJson;
        }

        private void StartGameWithJson(JsonResponseStructure jsonResponseStructure)
        {
            gameSessionManager.CreateGameSessionWithJson(jsonResponseStructure);
        }
    }
}