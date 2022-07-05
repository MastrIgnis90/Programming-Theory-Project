using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameUi : MonoBehaviour
{
    private PlayerController playerScript;
    [SerializeField] private int playerHealth = 0;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject healthPipPrefab;

    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(playerScript.playerHealth > playerHealth)
        {
            while (playerScript.playerHealth != healthBar.transform.childCount)
            {
                Instantiate(healthPipPrefab, healthBar.transform);
            }
            playerHealth = playerScript.playerHealth;
        }
        else if (playerScript.playerHealth < playerHealth)
        {
            for(int i  = 0; i < playerHealth - playerScript.playerHealth; i++)
            {
                DestroyImmediate(healthBar.transform.GetChild(0).gameObject);
            }
            playerHealth = playerScript.playerHealth;
        }
    }


}
