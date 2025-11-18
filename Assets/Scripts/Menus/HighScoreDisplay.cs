/*
A class to retrieve the user high score and display it in a text box.
This component must be attached to a TMP Component.
*/

using System.Collections;
using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    private TMP_Text _highScoreText;
    private ScoreJsonWriter _jsonScoreWriter => ImmortalSingleton<ScoreJsonWriter>.Instance;

    /// <summary>
    /// This component must be attached to a TMP_Text component.
    /// Score is handled by a ScoreJsonWriter. We wait for it to be created to move on.
    /// </summary>
    #region Monobehaviour methods
    void Awake()
    {
        _highScoreText = GetComponent<TMP_Text>();
        if (_highScoreText == null)
        {
            Debug.LogError("HighScoreDisplay requires a TMP_Text component.");
            enabled = false;
        }
        StartCoroutine(WriteHighScoreText());
    }
    #endregion

    private IEnumerator WriteHighScoreText()
    {
        yield return new WaitUntil(() => _jsonScoreWriter != null);
        int score = _jsonScoreWriter.GetHighScore();
        _highScoreText.text = $"High Score: {score}";
    }
}
