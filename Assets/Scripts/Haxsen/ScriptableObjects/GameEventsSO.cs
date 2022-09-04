using Haxsen.DataStructures;
using UnityEngine;
using UnityEngine.Events;

namespace Haxsen.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameEventsSO", menuName = "ScriptableObjects/GameEvents", order = 0)]
    public class GameEventsSO : ScriptableObject
    {
        public UnityAction<JsonResponseStructure> OnJsonReceived;
    }
}