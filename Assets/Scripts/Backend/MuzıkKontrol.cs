using UnityEngine;

public class MuzikKontrol : MonoBehaviour
{
    private static MuzikKontrol sistem;

    void Awake()
    {
        if (sistem != null)
        {
            Destroy(gameObject);
            return;
        }

        sistem = this;
        DontDestroyOnLoad(gameObject);
    }
}