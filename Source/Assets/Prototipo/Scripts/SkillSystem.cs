using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSystem : MonoBehaviour
{
    public GameManager gameManager;
    public Button MoreHealthButton;
    public Button MoreDamageButton;
    public Button SangradoButton;
    public Button VelAtaqueButton;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void MoreHealth(int health)
    {
        float newHealth = gameManager.playerStats.currentHealth;
        Debug.Log(newHealth);
        newHealth += health;
        gameManager.playerStats.currentHealth = newHealth;
        gameManager.playerStats.startingHealth = newHealth;
        Debug.Log(gameManager.playerStats.currentHealth);

        MoreHealthButton.interactable = false;
    }
}
