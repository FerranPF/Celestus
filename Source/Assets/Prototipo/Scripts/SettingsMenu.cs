using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour {
	public AudioMixer audioMixer;
	public AudioMixer fxMixer;
	//public Dropdown resolutionDropdown;
	Resolution[] resolutions;

    public Slider fxSlider;
    public Slider musicSlider;

	void Start(){
        fxSlider.value = PlayerPrefs.GetFloat("fxVol");
        musicSlider.value = PlayerPrefs.GetFloat("musicVol");
	}

	public void SetResolution720(){
		Screen.SetResolution(1280, 720, Screen.fullScreen);
	}

    public void SetResolution1200()
    {
        Screen.SetResolution(1600, 1200, Screen.fullScreen);
    }

    public void SetResolution1080()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("musicVol", volume);
        audioMixer.SetFloat("musicVolume", volume);
	}

    public void SetFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("fxVol", volume);
        fxMixer.SetFloat("fxVolume", volume);
    }

    public void SetQuality(int qualityIndex){
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void SetFullscreen(bool isFullscreen){
		Screen.fullScreen = isFullscreen;
	}
}
