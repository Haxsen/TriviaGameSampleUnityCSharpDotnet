using UnityEngine;

namespace Haxsen.Singleton
{
    /// <summary>
    /// Quite basic Singleton of custom type that is kept persistent through the scene changes.
    /// </summary>
    /// <typeparam name="T">The desired type to persist</typeparam>
    public class PersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance)
                    return _instance;
            
                new GameObject(typeof(T).ToString()).AddComponent<T>();
                DontDestroyOnLoad(_instance);
            
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else if (_instance != this)
            {
                Destroy(this);
            }
        }
    
        protected virtual void OnDestroy()
        {
            if (_instance != this)
            {
                return;
            }

            _instance = null;
        }
    }
}
