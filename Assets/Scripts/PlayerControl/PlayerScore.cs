/*
An object to keep a trace of the player score and update it.
*/

using TMPro;

public class PlayerScore : Singleton<PlayerScore>
{
    public TMP_Text ScoreText;
    private int _playerScore = 0;

    public int GetScore()
    {
        return _playerScore;
    }

    public void IncrementScore(int value)
    {
        _playerScore += value;
    }

    void Update()
    {
        ScoreText.text = $"Score: {_playerScore}";
    }
}
