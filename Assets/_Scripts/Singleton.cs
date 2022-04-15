using System.Threading.Tasks;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    // Start is called before the first frame update
    private static T instance;
    public static T Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<T> ();
                if (instance == null) {
                    GameObject obj = new GameObject
                    {
                        name = typeof(T).Name
                    };
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
        protected set => instance = value;
    }
 
    protected virtual void Awake ()
    {
        if (instance == null) {
            instance = this as T;
            DontDestroyOnLoad (gameObject);
        } else {
            Destroy (gameObject);
        }
    }
}
