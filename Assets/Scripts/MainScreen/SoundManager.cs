using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource musicsource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy when loading a new scene
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    private void Start()
    {
        // Load saved volume when the scene starts
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f); // Default volume is 1
        musicsource.volume = savedVolume;
    }

    public void SetMusicVolum(float volume)
    {
        musicsource.volume = volume;

        // Save the volume value
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save(); // Ensure it's saved persistently
    }
}
