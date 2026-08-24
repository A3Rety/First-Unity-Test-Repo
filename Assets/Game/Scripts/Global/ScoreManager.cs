using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Singleton { get; private set; }

    public event System.Action OnScoreChanged;

    private int _score;
    public int Score 
    {
        get => _score;
        private set 
        {
            if (value <= 0)
                value = 0;

            _score = value;

            OnScoreChanged?.Invoke();
        }
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

        Score = 0;
    }

    public void AddScore(int score)
    {
        Score += score;
    }

    public void ResetScore()
    {
        Score = 0;
    }
}
