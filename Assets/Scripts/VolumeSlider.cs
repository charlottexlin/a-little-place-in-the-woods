using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private MyAudioSource audioSource;
    private Slider slider;

    private void Start() {
        slider = GetComponent<Slider>();
    }

    public void ChangeVolume() {
        switch (audioSource) {
            case MyAudioSource.Music:
                Audio.instance.SetMusicVolume(slider.value);
                break;
            case MyAudioSource.Ambience:
                Audio.instance.SetAmbienceVolume(slider.value);
                break;
            case MyAudioSource.SFX:
                Audio.instance.SetSFXVolume(slider.value);
                break;
            default:
                break;
        }
    }

    private enum MyAudioSource {
        Music,
        Ambience,
        SFX
    }
}