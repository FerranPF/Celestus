using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Animator anim;

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
        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Prototipo");
    }
}
