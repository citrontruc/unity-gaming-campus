/*
An object to keep a trace of the player score and update it.
*/

public class PlayerScore : Singleton<PlayerScore>
{
    private int _playerScore = 0;
    public int GetScore()
    {
        return _playerScore;
    }

    public void IncrementScore(int value)
    {
        _playerScore += value;
    }
}