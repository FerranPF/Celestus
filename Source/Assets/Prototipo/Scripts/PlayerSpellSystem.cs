using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellSystem : MonoBehaviour
{
    bool canSpell = true;
    bool setTarget = false;

    bool target01 = false;
    float spell1Time;
    public float spell1Mana;
    public GameObject spell1Sprite;
    GameManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spell1Sprite.SetActive(false);
    }

    private void Update()
    {
        if (canSpell)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                target01 = true;
            }
        }

        if (target01)
        {
            SettingTarget();
            ShowSpell1();
            if (setTarget)
            {
                manager.playerStats.UseMana(spell1Mana);
                StartCoroutine(CastLightningSpell());
                spell1Sprite.SetActive(false);
            }
        }

    }

    void ShowSpell1()
    {
        spell1Sprite.SetActive(true);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, 1.3f, 0));
        float rayLength;
        Vector3 pointToLook;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            pointToLook = ray.GetPoint(rayLength);
            pointToLook = new Vector3(pointToLook.x, 1.3f, pointToLook.z);
            spell1Sprite.transform.LookAt(pointToLook);
        }
    }

    void SettingTarget()
    {
        if (Input.GetMouseButton(0))
        {
            GetTarget();
            setTarget = true;
            spell1Sprite.SetActive(false);
        }
    }

    IEnumerator CastLightningSpell()
    {
        manager.playerController.anim.SetBool("attack", true);
        canSpell = false;
        yield return new WaitForSeconds(spell1Time);
        manager.playerController.anim.SetBool("attack", false);
        setTarget = false;
        target01 = false;
        canSpell = true;
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
            manager.playerController.transform.LookAt(pointToLook);
        }
    }

}
