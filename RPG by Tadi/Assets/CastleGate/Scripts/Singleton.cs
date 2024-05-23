
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Ins
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    Debug.LogError($"No instance of {typeof(T)} found in the scene.");
                }
            }

            return instance;
        }
    }

    protected void Awake()
    {
        // Check if an instance already exists
        if (instance != null && instance != this)
        {
            // If an instance already exists and it's not this one, destroy this instance
            Destroy(gameObject);

            return;
        }

        instance = GetComponent<T>();

        if (transform.parent != null && transform.root != null)
        {
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Ensure the instance is destroyed when the application quits
    protected virtual void OnApplicationQuit()
    {
        instance = null;
    }
}
