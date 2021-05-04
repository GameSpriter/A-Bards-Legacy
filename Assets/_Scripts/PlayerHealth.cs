using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject player;
    int playerHealth = 5;

    void Update()
    {
        if (playerHealth == 0)
        {
            SceneManager.LoadScene("DungeonTest", LoadSceneMode.Single);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "EighthNoteEnemy")
        {
            playerHealth -= 1;
            Debug.Log("Current player health is: " + playerHealth);
        }
    }
}