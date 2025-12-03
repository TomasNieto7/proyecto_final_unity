using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void StartGame()
    {
       SceneManager.LoadScene("SampleScene");
    }
}
