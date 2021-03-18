using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool inRange = false;
    public GameObject player;
    public GameObject enemy;
    public Vector2 playerPosition;
    public Vector2 enemyPosition;
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
        float enemySpeed = speed * Time.deltaTime;
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
            //Debug.Log("Distance greater than 5");
        }
    }

    void combatWithPlayer()
    {
        float enemySpeed = speed * Time.deltaTime;

        if (inRange == true && Distance < 5 && Distance > 3)
        {
            transform.position = enemyPosition;
            //Debug.Log("Distance less than 5 and greater than 3");
        }
        else if (inRange == true && Distance < 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, enemySpeed);
            //Debug.Log("Distance less than 3, will now be moving towards player");
        }
        else
        {
            checkForPlayer();
        }
    }
}
