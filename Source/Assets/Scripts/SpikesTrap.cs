using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesTrap : MonoBehaviour
{
    private SpikesDamage damageColl;

    public bool activated;
    public float timeToDamage;
    private float cont = 0.0f;
    private bool CD = false;
    public float CDTime;

    private void Start()
    {
        damageColl = GetComponentInChildren<SpikesDamage>();
    }

    private void Update()
    {
        if (!CD)
        {
            damageColl.MyCollision();
            if (damageColl.start)
            {
                cont += Time.deltaTime;
                if (cont >= timeToDamage)
                {
                    damageColl.damage = true;
                    cont = 0.0f;
                    damageColl.start = false;
                    CD = true;
                }
            }
        }else if (CD)
        {
            cont += Time.deltaTime;
            if(cont >= CDTime)
            {
                cont = 0.0f;
                CD = false;
            }
        }
        
    }


}
