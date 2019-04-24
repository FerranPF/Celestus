using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    public PlayerSpellSystem playerSpellSystem;

    public Image Spell1;
    public Image Spell2;
    public Image Spell3;

    public float CDSpell1;
    public float CDSpell2;
    public float CDSpell3;

    private bool spell1;
    private bool spell2;
    private bool spell3;

    private void Start()
    {
        spell1 = false;
        spell2 = false;
        spell3 = false;
    }

    private void Update()
    {
        if (spell1)
        {
            Spell1.fillAmount -= (1 / CDSpell1)*Time.deltaTime;
            if (Spell1.fillAmount <= 0)
            {
                Spell1.fillAmount = 0.0f;
                spell1 = false;
                playerSpellSystem.canSpell1 = true;
            }
        }

        if (spell2)
        {
            Spell2.fillAmount -= (1 / CDSpell2) * Time.deltaTime;
            if (Spell2.fillAmount <= 0)
            {
                Spell2.fillAmount = 0.0f;
                spell2 = false;
                playerSpellSystem.canSpell2 = true;
            }
        }

        if (spell3)
        {
            Spell3.fillAmount -= (1 / CDSpell3)*Time.deltaTime;
            if (Spell3.fillAmount <= 0)
            {
                Spell3.fillAmount = 0.0f;
                spell3 = false;
                playerSpellSystem.canSpell3 = true;
            }
        }
    }

    public void Spell1CD()
    {
        spell1 = true;
        Spell1.fillAmount = 1.0f;
        playerSpellSystem.canSpell1 = false;
    }

    public void Spell2CD()
    {
        spell2 = true;
        Spell2.fillAmount = 1.0f;
        playerSpellSystem.canSpell2 = false;
    }

    public void Spell3CD()
    {
        spell3 = true;
        Spell3.fillAmount = 1.0f;
        playerSpellSystem.canSpell3 = false;
    }

}
