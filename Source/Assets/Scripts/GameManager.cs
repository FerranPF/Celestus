using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerController playerController;
    public GameObject skillTree;

    public Animator fadeAnim;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI expText;

    public AudioMixer fxMixer;
    public AudioMixer musicMixer;

    public int turretCont = 0;
    public int turretEnemies = 4;
    public float eventTextTime = 2.0f;
    public string eventText;
    public Text eventTextComponent;


    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        fxMixer.SetFloat("fxVolume", PlayerPrefs.GetFloat("fxVol"));
        musicMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVol"));
    }

    public void KillTurret()
    {
        turretCont++;
        if(turretCont == turretEnemies)
        {
            StartCoroutine(EventText());
        }
    }

    IEnumerator EventText()
    {
        eventTextComponent.enabled = true;
        eventTextComponent.text = eventText;
        playerStats.key = true;

        yield return new WaitForSeconds(eventTextTime);

        eventTextComponent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateCanvas();
    }

    /*
    void UpdateCanvas()
    {
        healthText.text = playerStats.currentHealth.ToString() + " / " + playerStats.startingHealth.ToString();
        manaText.text = playerStats.currentMana.ToString() + " / " + playerStats.startingMana.ToString();
        expText.text = playerStats.currentExp.ToString() + " / " + playerStats.maxExp.ToString();
    }
    
    public void OpenSkillTree()
    {
        skillTree.SetActive(true);
    }

    public void CloseSkillTree()
    {
        skillTree.SetActive(false);
    }
    */

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
