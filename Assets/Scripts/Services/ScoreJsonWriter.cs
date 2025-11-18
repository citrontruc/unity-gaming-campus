using System.IO;
using UnityEngine;

public class ScoreJsonWriter : ImmortalSingleton<ScoreJsonWriter>
{
    #region Event Channels
    [SerializeField]
    private HighScoreEventChannelSO _highScoreEventChannelSO;
    #endregion

    #region Read JSON HighScore
    private string _jsonDirectory = "Assets/Data/highscore.json";

    public class HighScoreJson
    {
        public int HighScore;
    }
    #endregion

    private int _highScore = 0;

    public int GetHighScore()
    {
        return _highScore;
    }

    void OnEnable()
    {
        ReadScoreFromJson();
        _highScoreEventChannelSO.onEventRaised += WriteScoreToJson;
    }

    void OnDisable()
    {
        _highScoreEventChannelSO.onEventRaised -= WriteScoreToJson;
    }

    public void ReadScoreFromJson()
    {
        string jsonString = File.ReadAllText(_jsonDirectory);
        HighScoreJson highScoreJson = JsonUtility.FromJson<HighScoreJson>(jsonString);
        _highScore = highScoreJson.HighScore;
        Debug.Log(_highScore);
    }

    public void WriteScoreToJson(int highScore)
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
