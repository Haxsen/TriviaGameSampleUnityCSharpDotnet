﻿using Haxsen.DataObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Haxsen.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameEventsSO", menuName = "ScriptableObjects/GameEvents", order = 0)]
    public class GameEventsSO : ScriptableObject
    {
        public UnityAction<JsonResponseQuestionStructure> OnJsonQuestionsReceived;
        public UnityAction<JsonResponseCategoryStructure> OnJsonCategoriesReceived;
        public UnityAction OnGameSessionCompleted;
        public UnityAction OnOpenTdbRequestStarted;
        public UnityAction OnOpenTdbRequestResultSuccess;
        public UnityAction OnOpenTdbRequestResultFail;
    }
}