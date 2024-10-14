using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _compAudioMixer;
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSfx;

    [SerializeField] private VolumeConfig masterVolumeConfig;
    [SerializeField] private VolumeConfig musicVolumeConfig;
    [SerializeField] private VolumeConfig sfxVolumeConfig;

    void Start()
    {
        sliderMaster.value = masterVolumeConfig.volume;
        sliderMusic.value = musicVolumeConfig.volume;
        sliderSfx.value = sfxVolumeConfig.volume;

        sliderMaster.onValueChanged.AddListener(SetMasterVolume);
        sliderMusic.onValueChanged.AddListener(SetMusicVolume);
        sliderSfx.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float value)
    {
        masterVolumeConfig.volume = Mathf.Clamp(value, 0f, 1f);
        _compAudioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
    }

    public void SetMusicVolume(float value)
    {
        musicVolumeConfig.volume = Mathf.Clamp(value, 0f, 1f);
        _compAudioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
    }

    public void SetSFXVolume(float value)
    {
        sfxVolumeConfig.volume = Mathf.Clamp(value, 0f, 1f);
        _compAudioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }
}