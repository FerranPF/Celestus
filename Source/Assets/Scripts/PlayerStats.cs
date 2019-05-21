using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

    private GameManager manager;

    public Image healthSlider;
    public Image manaSlider;
    public Image expSlider;
    public Image vignetteImage;

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
    public AnimationClip deathAnim;

    public bool key = false;

    [Header("Camera Shake")]
    private CameraShake shake;
    public float shakeMagnitude = .05f;
    public float shakeDuration = .14f;

    private void Awake()
    {
        shake = Camera.main.GetComponent<CameraShake>();
        currentHealth = startingHealth;
        currentMana = startingMana;
        playerControl = GetComponent<PlayerController>();
        level = 1.0f;
        expSlider.fillAmount = 0.0f;
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        vignetteImage.CrossFadeAlpha(0, 0, false);
    }

    public void TakeDamage(float amount)
    {
        StartCoroutine(AttackVignette());
        StartCoroutine(shake.Shake(shakeDuration, shakeMagnitude));
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
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

    IEnumerator Death()
    {
        playerControl.enabled = false;
        playerControl.anim.SetBool("death", true);
        yield return new WaitForSeconds(deathAnim.length);
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

    IEnumerator AttackVignette()
    {
        //vignetteImage.color = new Color(255, 255, 255, 0.05f);
        vignetteImage.CrossFadeAlpha(1, 0.2f, false);
        yield return new WaitForSeconds(0.5f);

        //vignetteImage.color = new Color(255, 255, 255, 0f);
        vignetteImage.CrossFadeAlpha(0.0f, 0.2f, false);
    }
}
