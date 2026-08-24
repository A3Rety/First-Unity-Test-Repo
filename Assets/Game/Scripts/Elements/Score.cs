using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI _text;


    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();

        ScoreManager.Singleton.OnScoreChanged += ChangeScore;
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
