using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Animator anim;
    public GameObject optionsPanel;
    public GameObject mainPanel;
    public GameObject fadePanel;

    void Awake(){
        fadePanel.SetActive(true);
    }

    void Start(){
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    public void ExitButtons()
    {
        Application.Quit();
    }

    // Update is called once per frame
    public void OptionsButton() {
		optionsPanel.SetActive(true);
        mainPanel.SetActive(false);
	}

    public void CreditsButton()
    {

    }

    public void OptionsBackButton()
    {
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);
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
