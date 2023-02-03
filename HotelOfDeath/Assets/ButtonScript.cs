using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{

    public void GoToScene(string buttonString)
    {
        SceneManager.LoadScene(buttonString);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
