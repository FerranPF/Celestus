using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerController playerController;
    public Sword sword;
    public GameObject skillTree;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSkillTree()
    {
        skillTree.SetActive(true);
    }

    public void CloseSkillTree()
    {
        skillTree.SetActive(false);
    }
}
