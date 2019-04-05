using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour {
	public AudioMixer audioMixer;
	//public Dropdown resolutionDropdown;
	Resolution[] resolutions;

	void Start(){
        /*
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions();

		List<string> options = new List<string>();

		int currentResolutionIndex = 0;

		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i] + " x " + resolutions[i].height;
			options.Add(option);

			if(resolutions[i].width == Screen.currentResolution.width && 
				resolutions[i].height == Screen.currentResolution.height){
					currentResolutionIndex = i;
				}
		}

		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
        */
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

    public void SetVolume(float volume){
		audioMixer.SetFloat("volume", volume);
	}

	public void SetQuality(int qualityIndex){
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void SetFullscreen(bool isFullscreen){
		Screen.fullScreen = isFullscreen;
	}
}
