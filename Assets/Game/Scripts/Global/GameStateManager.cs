using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Singleton { get; private set; }

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _loseSound;



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

        _audioSource = GetComponent<AudioSource>();
    }

    public void WinGame()
    {
        EndGame();

        TextMeshProUGUI title = GameObject.Find("Text_GameState").GetComponent<TextMeshProUGUI>();
        title.text = "Victory!";

        _audioSource.PlayOneShot(_winSound);
    }

    public void LoseGame()
    {
        EndGame();

        TextMeshProUGUI title = GameObject.Find("Text_GameState").GetComponent<TextMeshProUGUI>();
        title.text = "Game Over!";

        _audioSource.PlayOneShot(_loseSound);
    }

    private void EndGame()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        foreach (var obj in allEnemies)
        {
            obj.GetComponent<EnemyMovement>().enabled = false;
            obj.GetComponent<NavMeshAgent>().enabled = false;
        }

        player.GetComponent<PlayerController>().enabled = false;
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
            WinGame();
    }

    private void OnDestroy()
    {
        ScoreManager.Singleton.OnScoreChanged -= Left;
    }
}
