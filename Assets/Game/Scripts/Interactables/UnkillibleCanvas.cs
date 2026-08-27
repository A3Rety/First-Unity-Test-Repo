using UnityEngine;

public class UnkillibleCanvas : MonoBehaviour
{
    private void Awake()
    {
        UnkillibleCanvas[] all = FindObjectsByType<UnkillibleCanvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var obj in all)
        {
            if (obj != this && obj.name == this.name)
            {
                Destroy(gameObject);
                return;
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}
