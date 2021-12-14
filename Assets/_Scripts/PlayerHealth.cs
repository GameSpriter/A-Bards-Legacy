using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject player;
    public Slider healthSlider;
    int playerHealth = 5;
    public AudioSource playerHit;

    private void Awake()
    {
        healthSlider.maxValue = playerHealth;
        healthSlider.value = healthSlider.maxValue;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (playerHealth == 0)
        {
            SceneManager.LoadScene("DungeonTest", LoadSceneMode.Single);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "8thNoteAttackHitbox")
        {
            playerHit.Play();
            playerHealth -= 1;
            healthSlider.value = playerHealth;
            Debug.Log("Current player health is: " + playerHealth);
        }
    }
}