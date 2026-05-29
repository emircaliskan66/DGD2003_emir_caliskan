using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        AudioListener.volume = PlayerPrefs.GetFloat("SoundVolume", 0.75f);

        if (audioSource != null)
        {
            audioSource.loop = true;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}