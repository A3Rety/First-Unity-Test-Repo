using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Singleton { get; private set; }
    public static bool IsPlaying { get; private set; }
    private bool _isRestartable;

    private GameObject _startCanvas;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _loseSound;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private void ResetStatic()
    {
        var oldRef = Singleton != null ? Singleton.gameObject : null;
        Singleton = null;
        IsPlaying = false;

#if UNITY_EDITOR
        if (oldRef != null && oldRef.scene.name == "DontDestroyOnLoad")
        {
            Object.DestroyImmediate(oldRef.gameObject);
        }
#endif
    }

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

        _audioSource = GetComponent<AudioSource>();
        IsPlaying = false;
        _isRestartable = false;
    }

    private void Start()
    {
        FindStartCanvas();
        ScoreManager.Singleton.OnScoreChanged += Left;
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("pis");
        _isRestartable = false;
        StopGame();
    }

    public void StartGame()
    {
        UseStartCanvas(false);
        IsPlaying = true;
        Time.timeScale = 1f;
    }

    public void StopGame()
    {
        IsPlaying = false;
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        if (_isRestartable == false) return;

        _isRestartable = false;
        IsPlaying = false;
        UseStartCanvas(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        IsPlaying = false;
        _isRestartable = true;

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        TextMeshProUGUI restartText = GameObject.Find("Text_Restart").GetComponent<TextMeshProUGUI>();

        foreach (var obj in allEnemies)
        {
            obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            obj.GetComponent<EnemyMovement>().enabled = false;
            obj.GetComponent<NavMeshAgent>().enabled = false;
        }

        player.GetComponent<PlayerController>().enabled = false;
        restartText.text = "Press R for Restart game";
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

    private void UseStartCanvas(bool state)
    {
        _startCanvas.SetActive(state);
    }

    private void FindStartCanvas()
    {
        _startCanvas = GameObject.Find("Panel_StartMenu");
    }

    public void Left()
    {
        int count = FindActivePickups();
        Debug.Log("Осталось собрать: " + count);

        if (count <= 0)
            WinGame();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        ScoreManager.Singleton.OnScoreChanged -= Left;
    }
}
