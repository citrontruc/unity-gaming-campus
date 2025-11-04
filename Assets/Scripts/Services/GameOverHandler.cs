/*
Handles the transition between scenes on player Death.
*/

using UnityEngine.SceneManagement;

public class GameOverHandler : Singleton<GameOverHandler>
{
    public string NextScene = "GameOverScene";
    public NoHealthEventChannelSO _noHealthEventChannelSO;

    #region Subscribe to events
    void OnEnable()
    {
        _noHealthEventChannelSO.onEventRaised += _transitionScene;
    }

    void OnDisable()
    {
        _noHealthEventChannelSO.onEventRaised -= _transitionScene;
    }
    #endregion

    private void _transitionScene(int value)
    {
        SceneManager.LoadScene(NextScene);
    }
}
