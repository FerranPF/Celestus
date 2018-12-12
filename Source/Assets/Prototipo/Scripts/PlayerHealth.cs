using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public Image healthSlider;
    public Image manaSlider;
    public Image expSlider;
    public Image SpellCD;

    PlayerController playerControl;

    public float startingHealth = 100.0f;
    public float currentHealth;

    public float startingMana = 100.0f;
    public float currentMana;

    public int manaPotions;
    public int healthPotions;

    public float currentExp;
    public float maxExp = 100.0f;
    private float baseExp = 100.0f;
    public float level;

    public Animator fadeAnim;

    private void Awake()
    {
        currentHealth = startingHealth;
        currentMana = startingMana;
        playerControl = GetComponent<PlayerController>();
        level = 1.0f;
        expSlider.fillAmount = 0.0f;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Death();
        }
        healthSlider.fillAmount = currentHealth/startingHealth;
        Debug.Log("Health: " + currentHealth);
    }

    public void HealthRecovery(float amount)
    {
        if(currentHealth < startingHealth)
        {
            if(currentHealth + amount > startingMana)
            {
                currentHealth = startingHealth;
            }
            else
            {
                currentHealth += amount;
            }
            healthSlider.fillAmount = currentHealth/startingHealth;
        }
        Debug.Log("Health: " + currentHealth);
    }

    public void UseMana(float amount)
    {
        if(currentMana-amount < 0)
        {
            Debug.Log("You have not enougth mana");
        }
        else
        {
            currentMana -= amount;
            manaSlider.fillAmount = currentMana/startingMana;
        }
        Debug.Log("Mana: " + currentMana);
    }

    public void ManaRecovery(float amount)
    {
        if (currentMana < startingMana)
        {
            if(currentMana + amount > startingMana)
            {
                currentMana = startingMana;
            }
            else
            {
                currentMana += amount;
            }
            manaSlider.fillAmount = currentMana / startingMana;
        }
        Debug.Log("Mana: " + currentMana);
    }

    void Death()
    {
        playerControl.enabled = false;
        StartCoroutine(Fading());
    }

    public void GetExp(float amount)
    {
        currentExp += amount;

        if (currentExp >= maxExp)
        {
            currentExp -= baseExp * level;
            level++;
            maxExp = baseExp * level;
        }
        Debug.Log("Exp: " + currentExp + "MaxEsp: " + maxExp);
        expSlider.fillAmount = currentExp / maxExp;
    }

    IEnumerator Fading()
    {
        fadeAnim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("GameOver");
    }
}
