using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioMixer audioMixer;
    
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 20);
            Load();
        }
        else
        {
            Load();
        }
    }
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
        Save(volume);
    }
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void Save(float volume)
    {
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
}
