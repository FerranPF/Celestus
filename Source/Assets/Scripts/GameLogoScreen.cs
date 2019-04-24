using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogoScreen : MonoBehaviour {

    public Animator fadeAnim;
    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(Fading());
        }
    }

    IEnumerator Fading()
    {
        fadeAnim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MainMenu");
    }
}
