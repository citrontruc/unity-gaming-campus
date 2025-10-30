/*
A simple menu to start or quit game.
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGameButton()
    {
        SceneManager.LoadScene("Level1Scene");
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}
