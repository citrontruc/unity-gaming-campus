/*
A simple menu to start or quit game.
*/

using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private SceneTransitionManager _sceneTransitionManager => ImmortalSingleton<SceneTransitionManager>.Instance;

    public void PlayGameButton()
    {
        _sceneTransitionManager.LoadScene("Level1Scene");
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}
