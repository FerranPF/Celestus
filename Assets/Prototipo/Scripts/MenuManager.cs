using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    // Use this for initialization
    public void ExitButtons()
    {
        Application.Quit();
    }

    // Update is called once per frame
    public void OptionsButton() {
		
	}

    public void CreditsButton()
    {

    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Prototipo");
    }
}
