using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject enemy;
    int enemyHealth = 3;

    void Update()
    {
        if (enemyHealth == 0)
        {
            Destroy(enemy);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ShortSwordHitbox")
        {
            enemyHealth -= 1;
            Debug.Log("Current enemy health is: " + enemyHealth);
        }
    }
}
