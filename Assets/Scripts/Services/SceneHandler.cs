using UnityEngine;

public class SceneHandler : Singleton<SceneHandler>
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(NextScene);
    }
}
