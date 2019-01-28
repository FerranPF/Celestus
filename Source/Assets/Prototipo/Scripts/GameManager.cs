using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerController playerController;
    public Sword sword;
    public GameObject skillTree;

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
}
