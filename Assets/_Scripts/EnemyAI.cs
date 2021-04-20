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

    IEnumerator TimeForEnemyToMoveBack(float seconds)
    {
        float counter = seconds;
        while (counter > 0f)
        {
            yield return new WaitForSeconds(.25f);
            counter--;
        }
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
        }
    }

    void combatWithPlayer()
    {
        if (inRange == true && (Distance > 4))
        {
            transform.position = enemyPosition;
        }
        else if (inRange == true && (Distance < 4 && Distance > .85))
        {
            chasePlayer();
        }
        else if (inRange == true && (Distance < .65))
        {
            meleeAttackPlayer();
        }
    }

    void chasePlayer()
    {
        float enemySpeed = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, enemySpeed);
    }

    void meleeAttackPlayer()
    {
        //Attack code here, along with damage and animation
        StartCoroutine(TimeForEnemyToMoveBack(.1f));
        //Move enemy back
    }
}
