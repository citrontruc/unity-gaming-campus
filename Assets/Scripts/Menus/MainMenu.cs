/*
A simple menu to start or quit game.
*/

using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGameButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1Scene");
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}
