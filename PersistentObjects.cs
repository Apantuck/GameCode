using UnityEngine;

public class PersistentObjects : MonoBehaviour
{
    public static PersistentObjects instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(instance);
    }
}
