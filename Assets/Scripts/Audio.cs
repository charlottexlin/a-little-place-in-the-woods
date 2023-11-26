using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance { get; private set; }
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource ambience;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip waterDrop;
    [SerializeField] private AudioClip leafRustle;

    private void Awake() 
    {
        // Delete this instance if there is another instance
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    // Plays the sound effect clip with the given name
    public void PlaySound(string clip) {
        switch (clip) {
            case "click":
                sfx.clip = click;
                sfx.Play();
                break;
            case "waterDrop":
                sfx.clip = waterDrop;
                sfx.Play();
                break;
            case "leafRustle":
                sfx.clip = leafRustle;
                sfx.Play();
                break;
            default:
                break;
        }
    }
    
    public void ToggleMusic() {
        music.mute = !music.mute;
    }

    public void ToggleAmbience() {
        ambience.mute = !ambience.mute;
    }

    public void ToggleSFX() {
        sfx.mute = !sfx.mute;
    }

    public void SetMusicVolume(float volume) {
        music.volume = volume;
    }

    public void SetAmbienceVolume(float volume) {
        ambience.volume = volume;
    }

    public void SetSFXVolume(float volume) {
        sfx.volume = volume;
    }
}