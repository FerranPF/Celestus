using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

	public Animator fadeAnim;
	public float logoTime;

	void Start () {
        logoTime = 5.0f;
	}

	void Update(){
		logoTime -= Time.deltaTime;
		if(logoTime <= 0){
			StartCoroutine(Fading());
		}
	}

	IEnumerator Fading()
    {
        fadeAnim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("ProtoMenu");
    }
}
