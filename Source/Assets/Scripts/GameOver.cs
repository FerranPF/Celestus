using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public string scene;

	public void MainMenu(){
		SceneManager.LoadScene(scene);
	}
}
