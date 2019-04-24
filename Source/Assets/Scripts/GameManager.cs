using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCanvas();
    }

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
