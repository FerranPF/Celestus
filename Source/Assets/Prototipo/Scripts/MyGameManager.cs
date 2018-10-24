using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour {


	public Image SpellCD;
    public Image HealthFill;
	private float count;
    private float totalHealth = 1f;
	
	// Use this for initialization
	void Start () {
		count = 0;
        HealthFill.fillAmount = totalHealth;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (count <= 0)
            {
               SpellCoolDown();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if(totalHealth > 0f)
            {
                GetDamage(0.1f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (totalHealth < 1f)
            {
                GetHealth(0.1f);
            }
        }



        count -= Time.deltaTime;
        if(count <= 0)
        {
            count = 0;
        }
        SpellCD.fillAmount = count;
	}

    void GetDamage(float damage)
    {
        totalHealth -= damage;
        HealthFill.fillAmount = totalHealth;

    }

    void GetHealth(float health)
    {
        totalHealth += health;
        HealthFill.fillAmount = totalHealth;
    }
    void SpellCoolDown()
    {
        count = 1;
    }
}
