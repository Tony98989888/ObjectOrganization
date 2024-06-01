using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<T>();
                if (m_instance == null && !Application.isPlaying)
                    Debug.LogError($"No instance of {typeof(T)} found in the scene and not in play mode. " +
                                   "Make sure you have an instance of {typeof(T)} in your scene or create one at runtime.");
            }

            return m_instance;
        }
    }

    protected virtual void OnDestroy()
    {
        if (m_instance == this) m_instance = null;
    }
}