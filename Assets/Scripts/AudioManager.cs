using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;

            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource titleMusic, levelMusic, bossMusic, winMusic;

    public AudioSource[] sfx;

    public void StopMusic()
    {
        titleMusic.Stop();
        levelMusic.Stop();
        bossMusic.Stop();
        winMusic.Stop();
    }

    public void PlayTitleMusic()
    {
        StopMusic();

        titleMusic.Play();
    }

    public void PlayLevelMusic()
    {
        StopMusic();

        levelMusic.Play();
    }

    public void PlayBossMusic()
    {
        StopMusic();

        bossMusic.Play();
    }

    public void PlayWinMusic()
    {
        StopMusic();

        winMusic.Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();

        sfx[sfxToPlay].Play();
    }
}
