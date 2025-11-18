using System.IO;
using UnityEngine;

public class ScoreJsonWriter : ImmortalSingleton<ScoreJsonWriter>
{
    #region Read JSON HighScore
    private string _jsonDirectory = "Assets/Data/highscore.json";

    public class HighScoreJson
    {
        public int HighScore;
    }
    #endregion

    private int _highScore = 0;

    [SerializeField]
    private HighScoreEventChannelSO _highScoreEventChannelSO;

    void OnEnable()
    {
        ReadScore();
        _highScoreEventChannelSO.onEventRaised += WriteScore;
    }

    void OnDisable()
    {
        _highScoreEventChannelSO.onEventRaised -= WriteScore;
    }

    public void ReadScore()
    {
        string jsonString = File.ReadAllText(_jsonDirectory);
        HighScoreJson highScoreJson = JsonUtility.FromJson<HighScoreJson>(jsonString);
        _highScore = highScoreJson.HighScore;
        Debug.Log(_highScore);
    }

    public void WriteScore(int highScore)
    {
        if (highScore > _highScore)
        {
            _highScore = highScore;
            HighScoreJson highScoreJson = new();
            highScoreJson.HighScore = _highScore;
            string jsonString = JsonUtility.ToJson(highScoreJson);
            File.WriteAllText(_jsonDirectory, jsonString);
        }
    }
}
