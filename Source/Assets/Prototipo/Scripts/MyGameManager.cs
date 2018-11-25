using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour {

    public GameObject pausePanel;
	public GameObject statsPanel;
    public Animator fadeAnim;
	
	private bool isPaused;
	
    
    void Start () {
		isPaused = false;
		statsPanel.SetActive(true);
		pausePanel.SetActive(false);
	}    

    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(!isPaused){
				isPaused = true;
				Pause();
			}else{
				isPaused = false;
				Resume();
			}
		}
	}
	
	public void Pause(){
		pausePanel.SetActive(true);
		statsPanel.SetActive(false);
		Time.timeScale = 0.0f;
	}
	
	public void Resume(){
		pausePanel.SetActive(false);
		statsPanel.SetActive(true);
		Time.timeScale = 1.0f;
	}
	
	public void ExitMenu(){
		Time.timeScale = 1.0f;
        StartCoroutine(Fading());
	}
	
	public void Exit(){
		Application.Quit();
	}

    IEnumerator Fading()
    {
        fadeAnim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("ProtoMenu");
    }
}
