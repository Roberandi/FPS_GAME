using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;

    private void Start()
    {
        AudioManager.instance.PlayTitleMusic();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);

        AudioManager.instance.PlayLevelMusic();
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quitting Game");
    }
}
