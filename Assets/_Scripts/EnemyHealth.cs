using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int enemyHealth = 10;

    public GameObject arrow;

    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ShortSwordHitbox")
        {
            enemyHealth -= 1;
            Debug.Log("Current enemy health is: " + enemyHealth);
        }
        if (col.gameObject.tag == "LongSwordHitbox")
        {
            enemyHealth -= 3;
            Debug.Log("Current enemy health is: " + enemyHealth);
        }
        if (col.gameObject.tag == "Arrow")
        {
            enemyHealth -= 1;
            Debug.Log("Current enemy health is: " + enemyHealth);
            Destroy(col.gameObject, 0f);
        }
    }
}
