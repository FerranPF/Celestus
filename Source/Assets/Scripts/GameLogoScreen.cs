using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameLogoScreen : MonoBehaviour {

    public Animator fadeAnim;
    AudioSource source;

    private void Awake()
    {
        source = GameObject.FindObjectOfType<AudioSource>();
        source.volume = 0.5f;
        
    }

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
