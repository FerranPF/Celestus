using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellSystem : MonoBehaviour
{
    public bool canSpell = true;
    bool setTarget = false;

    public AnimationClip spellAnim1;
    public AnimationClip spellAnim2;

    float spell1Time;
    float spell2Time;
    float spell3Time;

    public float spell1Mana;
    public float spell2Mana;
    public float spell3Mana;

    public bool canSpell1 = true;
    public bool canSpell2 = true;
    public bool canSpell3 = true;

    public GameObject spell1Sprite;
    public GameObject spell2Sprite;
    public GameObject spell3Sprite;

    public GameObject lightningSpell;
    public Object fireSpell;
    public GameObject iceSpell;
    Vector3 firePos;

    GameManager manager;
    SpellManager spellManager;

    public Transform lightningSpellSpawn;

    enum Spell
    {
        None,
        Lightning,
        Fire,
        Ice
    }

    Spell spellType;

    private void Start()
    {

        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spellManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpellManager>();
        spell1Sprite.SetActive(false);
        spell2Sprite.SetActive(false);
        spell3Sprite.SetActive(false);

        spell1Time = spellAnim1.length;
        spell2Time = spellAnim2.length;
        spell3Time = spellAnim1.length;

        spellType = Spell.None;
        canSpell = true;
    }

    private void Update()
    {
        if (manager.playerStats.currentMana <= 0)
        {
            Debug.Log("No Mana");
            canSpell = false;
        }

        if (canSpell)
        {
            if (Input.GetKey(KeyCode.Alpha1) && canSpell1)
            {
                ResetTarget();
                spellType = Spell.Lightning;
                Debug.Log(spellType);
            }

            if (Input.GetKey(KeyCode.Alpha2) && canSpell2)
            {
                ResetTarget();
                spellType = Spell.Fire;
                Debug.Log(spellType);
            }

            if (Input.GetKey(KeyCode.Alpha3) && canSpell3)
            {
                ResetTarget();
                spellType = Spell.Ice;
                Debug.Log(spellType);
            }
        }

        switch (spellType)
        {
            case Spell.Lightning:
                ShowSpell();
                SettingTarget(spell1Sprite, spell1Mana);
                if (setTarget)
                {
                    spellManager.Spell1CD();
                    StartCoroutine(CastLightningSpell());
                }
                break;

            case Spell.Fire:
                ShowSpell();
                SettingTarget(spell2Sprite, spell2Mana);
                if (setTarget)
                {
                    spellManager.Spell2CD();
                    StartCoroutine(CastFireSpell());
                }
                break;

            case Spell.Ice:
                ShowSpell();
                SettingTarget(spell3Sprite, spell3Mana);
                if (setTarget)
                {
                    spellManager.Spell3CD();
                    StartCoroutine(CastIceSpell());
                }
                break;

            case Spell.None:
                break;
            default:
                break;
        }
    }

    void ShowSpell()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, -1.0f, 0));
        float rayLength;
        Vector3 pointToLook;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            pointToLook = ray.GetPoint(rayLength);
            if (spellType == Spell.Lightning)
            {
                spell1Sprite.SetActive(true);
                Vector3 PoI = new Vector3(pointToLook.x - manager.playerController.transform.position.x, 0f, pointToLook.z - manager.playerController.transform.position.z);
                spell1Sprite.transform.rotation = Quaternion.LookRotation(PoI, Vector3.up);
            }

            if (spellType == Spell.Fire)
            {
                spell2Sprite.SetActive(true);
                Vector3 PoI = new Vector3(pointToLook.x, -1.0f, pointToLook.z);
                spell2Sprite.transform.position = PoI;
                firePos = pointToLook;
            }

            if (spellType == Spell.Ice)
            {
                spell3Sprite.SetActive(true);
                Vector3 PoI = new Vector3(pointToLook.x - manager.playerController.transform.position.x, 0f, pointToLook.z - manager.playerController.transform.position.z);
                spell3Sprite.transform.rotation = Quaternion.LookRotation(PoI, Vector3.up);
            }
        }
    }

    void SettingTarget(GameObject spell, float spellMana)
    {
        manager.playerController.canAttack = false;
        if (Input.GetMouseButtonDown(0))
        {
            manager.playerStats.UseMana(spellMana);
            GetTarget();
            setTarget = true;
            spell.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            ResetTarget();
        }
    }

    void ResetTarget()
    {
        spell1Sprite.SetActive(false);
        spell2Sprite.SetActive(false);
        spell3Sprite.SetActive(false);
        manager.playerController.canAttack = true;
        canSpell = true;
        setTarget = false;
        spellType = Spell.None;
    }

    public void InstSpell()
    {
        switch (spellType)
        {
            case Spell.Lightning:
                Instantiate(lightningSpell, lightningSpellSpawn.position, transform.rotation);
                break;

            case Spell.Fire:
                Instantiate(fireSpell, firePos, transform.rotation);
                break;

            case Spell.Ice:
                Instantiate(iceSpell, this.transform.position, transform.rotation);
                break;
            default:
                break;
        }
    }

    IEnumerator CastLightningSpell()
    {
        manager.playerController.anim.SetBool("spell", true);
        canSpell = false;
        manager.playerController.canMove = false;
        spell1Sprite.SetActive(false);

        yield return new WaitForSeconds(spell1Time * 0.8f);
        spellType = Spell.None;

        manager.playerController.anim.SetBool("spell", false);
        manager.playerController.canMove = true;
        ResetTarget();
    }

    IEnumerator CastFireSpell()
    {
        manager.playerController.anim.SetBool("spell2", true);
        canSpell = false;
        manager.playerController.canMove = false;
        spell2Sprite.SetActive(false);

        yield return new WaitForSeconds(spell2Time*0.6f);
        spellType = Spell.None;

        manager.playerController.anim.SetBool("spell2", false);
        manager.playerController.canMove = true;
        ResetTarget();
    }

    IEnumerator CastIceSpell()
    {
        manager.playerController.anim.SetBool("spell", true);
        manager.playerController.canMove = false;
        canSpell = false;
        spell3Sprite.SetActive(false);

        yield return new WaitForSeconds(spell3Time * 0.8f);
        spellType = Spell.None;

        manager.playerController.anim.SetBool("spell", false);
        manager.playerController.canMove = true;
        ResetTarget();
    }

    
    void GetTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, 1.3f, 0));
        float rayLength;
        Vector3 pointToLook;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            pointToLook = ray.GetPoint(rayLength);
            Vector3 PoI = new Vector3(pointToLook.x - manager.playerController.transform.position.x, 0f, pointToLook.z - manager.playerController.transform.position.z);
            manager.playerController.transform.rotation = Quaternion.LookRotation(PoI, Vector3.up);
        }
    }

}