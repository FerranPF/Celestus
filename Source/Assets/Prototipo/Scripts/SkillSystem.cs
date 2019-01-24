using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSystem : MonoBehaviour
{
    public GameManager gameManager;

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

        Close();
    }

    public void MoreDamage(int damage)
    {
        int newDamage = gameManager.sword.damage;
        Debug.Log(newDamage);
        newDamage += damage;
        gameManager.sword.damage = newDamage;
        Debug.Log(gameManager.sword.damage);

        Close();
    }

    public void Sangrado()
    {
        gameManager.sword.sangrado = true;
        Close();
    }

    private void Close()
    {
        this.gameObject.SetActive(false);
    }
}
