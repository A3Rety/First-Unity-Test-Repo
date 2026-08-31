using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI _text;


    private void Start()
    {
        ScoreManager.Singleton.OnScoreChanged += ChangeScore;

        _text = GetComponent<TextMeshProUGUI>();
    }

    private void ChangeScore()
    {
        _text.text = $"Score: {ScoreManager.Singleton.Score}";
    }

    private void OnDestroy()
    {
        ScoreManager.Singleton.OnScoreChanged -= ChangeScore;
    }
}
