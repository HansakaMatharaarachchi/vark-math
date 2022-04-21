using UnityEngine;

namespace _Scripts.Utils
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        // static reference to the current instance.
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null) return instance;
                instance = FindObjectOfType<T>();
                if (instance != null) return instance;
                GameObject obj = new GameObject
                {
                    name = typeof(T).Name
                };
                instance = obj.AddComponent<T>();
                return instance;
            }
            protected set => instance = value;
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                // makes the game object persist between scenes when loading a new scenes
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}