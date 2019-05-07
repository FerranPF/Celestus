using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    private Light torchLight;
    private float baseIntensity;
    public float randomTime;
    private void Awake()
    {
        torchLight = GetComponentInChildren<Light>();
        baseIntensity = torchLight.intensity;
        //Invoke("RandomLight", randomTime);
    }

    private void Update()
    {
        //InvokeRepeating("RandomLight", Time.deltaTime, randomTime);
        //Invoke("RandomLight", randomTime);
        StartCoroutine(RandomLight());
    }

    private IEnumerator RandomLight()
    {
        //torchLight.color = new Color(1-(Random.Range(0.8f, 1)), 0, 0, 1);
        yield return new WaitForSecondsRealtime(randomTime);
        torchLight.intensity = baseIntensity - Random.Range(2f, 5f);
    }
}
