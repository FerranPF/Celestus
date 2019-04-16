using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour {

    public GameObject pausePanel;
	public GameObject statsPanel;
	public GameObject fadePanel;
	public GameObject godPanel;

    public Animator fadeAnim;
	public PlayerController player;
	
	private bool godMode;
	private bool isPaused;
	
    void Awake(){
		fadePanel.SetActive(true);
	}
	
    void Start () {
		godMode = false;
		isPaused = false;
		statsPanel.SetActive(true);
		pausePanel.SetActive(false);
		godPanel.SetActive(false);
	}

    void Update () {

		if(Input.GetKeyDown(KeyCode.F10)){
			if(!godMode){
				godMode = true;
				godPanel.SetActive(true);
			}else{
				godMode = false;
				godPanel.SetActive(false);
			}
		}

		if(Input.GetKeyDown(KeyCode.Escape)){
			if(!isPaused){
				Pause();
			}else{
				Resume();
			}
		}
	}
	
	public void Pause(){
		player.canMove = false;
		isPaused = true;
		pausePanel.SetActive(true);
		statsPanel.SetActive(false);
		Time.timeScale = 0.0f;
	}
	
	public void Resume(){
		isPaused = false;
		pausePanel.SetActive(false);
		statsPanel.SetActive(true);
		Time.timeScale = 1.0f;
		player.canMove = true;
	}
	
	public void ExitMenu(){
		Time.timeScale = 1.0f;
        StartCoroutine(Fading("MainMenu"));
	}
	
	public void Exit(){
		Application.Quit();
	}

    public void Win()
    {
        StartCoroutine(Fading("Win"));
    }

    IEnumerator Fading(string scene)
    {
        fadeAnim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(scene);
    }
}
