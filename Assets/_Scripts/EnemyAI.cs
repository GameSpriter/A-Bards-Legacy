using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private bool inRange = false;
    public GameObject player;
    public GameObject enemy;
    private Vector2 playerPosition;
    private Vector2 enemyPosition;
    Vector2 Destination;
    float Distance;
    private float speed = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        checkForPlayer();
        playerPosition = player.transform.position;
        enemyPosition = enemy.transform.position;
    }

    void checkForPlayer()
    {
        Destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        Distance = Vector2.Distance(gameObject.transform.position, Destination);

        if (Distance < 5)
        {
            inRange = true;
            combatWithPlayer();
        }
        else
        {
            inRange = false;
            Debug.Log("Distance greater than 5");
        }
    }

    void combatWithPlayer()
    {
        if (inRange == true && (Distance < 5 && Distance > 3))
        {
            transform.position = enemyPosition;
        }
        else if (inRange == true && (Distance < 3 && Distance > 2))
        {
            chasePlayer();
        }
        else if (inRange == true && Distance < 1)
        {
            meleeAttackPlayer();
        }
        else
        {
            checkForPlayer();
        }
    }

    void chasePlayer()
    {
        float enemySpeed = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, enemySpeed);
    }

    void meleeAttackPlayer()
    {
        float enemySpeed = speed * Time.deltaTime;
        //Attack code here, along with damage and animation

        //Move enemy back 
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, -1 * enemySpeed);
    }
}
