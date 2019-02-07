using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellSystem : MonoBehaviour
{
    bool canSpell = true;
    bool setTarget = false;

    bool target01 = false;
    bool target02 = false;
    bool target03 = false;

    float spell1Time;
    float spell2Time;

    public float spell1Mana;
    public float spell2Mana;

    public GameObject spell1Sprite;
    public GameObject spell2Sprite;
    GameManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spell1Sprite.SetActive(false);
        spell2Sprite.SetActive(false);
        //spell3Sprite.SetActive(false);
    }

    private void Update()
    {
        if (manager.playerStats.currentMana <= 0)
        {
            canSpell = false;
        }

        if (canSpell)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                target01 = true;
                target02 = false;
                target03 = false;
                canSpell = false;
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                target02 = true;
                target01 = false;
                target03 = false;
                canSpell = false;
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                target01 = false;
                target02 = false;
                target03 = true;
                canSpell = false;
            }
        }

        if (target01)
        {
            SettingTarget1();
            ShowSpell1();
            if (setTarget)
            {
                StartCoroutine(CastLightningSpell());
            }
        }

        if (target02)
        {
            SettingTarget2();
            ShowSpell2();
            if (setTarget)
            {
                StartCoroutine(CastFireSpell());
            }
        }

    }

    void ShowSpell1()
    {
        spell1Sprite.SetActive(true);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, 0f, 0));
        float rayLength;
        Vector3 pointToLook;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            pointToLook = ray.GetPoint(rayLength);
            Vector3 PoI = new Vector3(pointToLook.x - manager.playerController.transform.position.x, 0f, pointToLook.z - manager.playerController.transform.position.z);
            spell1Sprite.transform.rotation = Quaternion.LookRotation( PoI, Vector3.up);
        }
    }

    void ShowSpell2()
    {
        spell2Sprite.SetActive(true);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));
        float rayLength;
        Vector3 pointToLook;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            pointToLook = ray.GetPoint(rayLength);
            Vector3 PoI = new Vector3(pointToLook.x, 0f, pointToLook.z);
            spell2Sprite.transform.position = PoI;
        }
    }

    void SettingTarget1()
    {
        if (Input.GetMouseButtonDown(0))
        {
            manager.playerStats.UseMana(spell1Mana);
            manager.playerController.canAttack = false;
            GetTarget();
            setTarget = true;
            spell1Sprite.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            ResetTarget();
        }
    }

    void SettingTarget2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            manager.playerStats.UseMana(spell2Mana);
            manager.playerController.canAttack = false;
            GetTarget();
            setTarget = true;
            spell2Sprite.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            ResetTarget();
        }
    }

    void ResetTarget()
    {
        //spell3Sprite.SetActive(false);
        manager.playerController.canAttack = true;
        canSpell = true;
        target01 = false;
        target02 = false;
        target03 = false;
        setTarget = false;

        spell1Sprite.SetActive(false);
        spell2Sprite.SetActive(false);
    }

    IEnumerator CastLightningSpell()
    {
        manager.playerController.anim.SetBool("attack", true);
        canSpell = false;
        yield return new WaitForSeconds(spell1Time);
        manager.playerController.anim.SetBool("attack", false);
        ResetTarget();
    }

    IEnumerator CastFireSpell()
    {
        manager.playerController.anim.SetBool("attack", true);
        canSpell = false;
        yield return new WaitForSeconds(spell2Time);
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
