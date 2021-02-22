using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public Vector2 playerPosition;
    public Vector2 enemyPosition;
    Vector2 Destination;
    float Distance;
    float moveSpeed = 2f;

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

        if (Distance < 4 && Distance > 2)
        {
            gameObject.transform.position = enemyPosition;
            Debug.Log("Distance less than 4 and greater than 2");
        }
        else if (Distance < 2)
        {
            gameObject.transform.position = playerPosition * .5f;
            Debug.Log("Distance less than 2");
        }
        else
        {
            Debug.Log("Distance greater than 4");
        }
    }
}
