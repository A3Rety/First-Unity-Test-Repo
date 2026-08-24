using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Singleton { get; private set; }
    

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(Singleton);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ScoreManager.Singleton.OnScoreChanged += Left;
    }

    public int FindActivePickups()
    {
        GameObject[] found = GameObject.FindGameObjectsWithTag("PickUp");
        int count = -1;

        foreach (var obj in found)
        {
            if (obj.activeInHierarchy)
            {
                count++;
            }
        }

        return count;
    }

    public void Left()
    {
        int count = FindActivePickups();
        Debug.Log("Осталось собрать: " + count);

        if (count <= 0)
            Debug.Log("Victory!");
    }

    private void OnDestroy()
    {
        ScoreManager.Singleton.OnScoreChanged -= Left;
    }
}
