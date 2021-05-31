using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    
    public AudioMixer audioMixer;

    public void SetGeneralVolume(float volume)
    {
        audioMixer.SetFloat("GeneralVolume", volume);
        SaveManager.instance.activeSave.generalVolume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        SaveManager.instance.activeSave.musicVolume = volume;
    }
    public void SetSoundsVolume(float volume)
    {
        audioMixer.SetFloat("SoundsVolume", volume);
        SaveManager.instance.activeSave.soundsVolume = volume;
    }
}
