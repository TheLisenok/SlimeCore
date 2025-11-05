using UnityEngine;

public class PersistentManagers : MonoBehaviour
{
    private static PersistentManagers instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            if (transform.parent != null)
                transform.SetParent(null); // на всякий случай, чтобы быть корневым

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
