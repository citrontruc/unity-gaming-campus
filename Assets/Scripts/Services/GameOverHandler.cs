/*
Handles the transition between scenes on player Death.
*/

public class GameOverHandler : Singleton<GameOverHandler>
{
    #region Event Channels
    public NoHealthEventChannelSO _noHealthEventChannelSO;
    #endregion

    #region Scene Transition Attributes
    public string NextScene = "GameOverScene";
    private SceneTransitionManager _sceneTransitionManager => ImmortalSingleton<SceneTransitionManager>.Instance;
    #endregion

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
        _sceneTransitionManager.LoadScene(NextScene);
    }
}
