using System.Collections;
using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    private TMP_Text _highScoreText;
    private ScoreJsonWriter _jsonScoreWriter => ImmortalSingleton<ScoreJsonWriter>.Instance;

    void Awake()
    {
        _highScoreText = GetComponent<TMP_Text>();
        StartCoroutine(WriteHighScoreText());
    }

    private IEnumerator WriteHighScoreText()
    {
        yield return new WaitUntil(() => _jsonScoreWriter != null);
        int score = _jsonScoreWriter.GetHighScore();
        _highScoreText.text = $"High Score: {score.ToString()}";
    }
}
