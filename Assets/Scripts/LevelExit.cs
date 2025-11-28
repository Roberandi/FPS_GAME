using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(sceneToLoad);

            AudioManager.instance.PlayLevelMusic();



            AudioManager.instance.PlaySFX(3);
        }
    }
}
