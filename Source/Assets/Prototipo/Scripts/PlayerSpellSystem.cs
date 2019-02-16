using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellSystem : MonoBehaviour
{
    bool canSpell = true;
    bool setTarget = false;

    float spell1Time;
    float spell2Time;
    float spell3Time;

    public float spell1Mana;
    public float spell2Mana;
    public float spell3Mana;

    public GameObject spell1Sprite;
    public GameObject spell2Sprite;
    public GameObject spell3Sprite;

    public ParticleSystem LightningPS;
    public Object firePrefab;
    Vector3 firePos;

    GameManager manager;

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
        spell1Sprite.SetActive(false);
        spell2Sprite.SetActive(false);
        spell3Sprite.SetActive(false);
        spellType = Spell.None;
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
            if (Input.GetKey(KeyCode.Alpha1))
            {
                ResetTarget();
                spellType = Spell.Lightning;
                Debug.Log(spellType);
            }

            if (Input.GetKey(KeyCode.Alpha2))
            {
                ResetTarget();
                spellType = Spell.Fire;
                Debug.Log(spellType);
            }

            if (Input.GetKey(KeyCode.Alpha3))
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
                    StartCoroutine(CastLightningSpell());
                }
                break;

            case Spell.Fire:
                ShowSpell();
                SettingTarget(spell2Sprite, spell2Mana);
                if (setTarget)
                {
                    StartCoroutine(CastFireSpell(firePos));
                }
                break;

            case Spell.Ice:
                ShowSpell();
                SettingTarget(spell3Sprite, spell3Mana);
                if (setTarget)
                {
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
        if (Input.GetMouseButtonDown(0))
        {
            manager.playerStats.UseMana(spellMana);
            manager.playerController.canAttack = false;
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

    IEnumerator CastLightningSpell()
    {
        manager.playerController.anim.SetBool("attack", true);
        canSpell = false;
        LightningPS.Play();
        yield return new WaitForSeconds(spell1Time);
        manager.playerController.anim.SetBool("attack", false);
        ResetTarget();
    }

    IEnumerator CastFireSpell(Vector3 position)
    {
        manager.playerController.anim.SetBool("attack", true);
        canSpell = false;
        yield return new WaitForSeconds(spell2Time);
        Instantiate(firePrefab, position, transform.rotation);
        manager.playerController.anim.SetBool("attack", false);
        ResetTarget();
    }

    IEnumerator CastIceSpell()
    {
        manager.playerController.anim.SetBool("attack", true);
        canSpell = false;
        yield return new WaitForSeconds(spell3Time);
        manager.playerController.anim.SetBool("attack", false);
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