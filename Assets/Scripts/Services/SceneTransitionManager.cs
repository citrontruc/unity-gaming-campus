/*
A class to handle scene transitions.
*/

using UnityEngine.SceneManagement;

public class SceneTransitionManager : ImmortalSingleton<SceneTransitionManager>
{
    private string _currentScene => SceneManager.GetActiveScene().name;

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
