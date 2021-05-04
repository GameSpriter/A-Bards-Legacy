using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private bool inRange = false;
    private GameObject player;
    private Vector2 playerPosition;
    private Vector2 enemyPosition;
    Vector2 Destination;
    float Distance;
    private float speed = 2f;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        if(player == null) {
            player = Camera.main.GetComponent<CameraRefs>().player;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /*
    IEnumerator unFreezePosition(float seconds)
    {
        float counter = seconds;
        speed = 0f;
        
        while (counter > 0f)
        {
            float enemySpeed = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, enemySpeed);
            transform.position = enemyPosition;
            Debug.Log("Frozen");
            yield return new WaitForSeconds(5f);
            counter--;
        }
    }
    */

    void Update()
    {
        checkForPlayer();
        playerPosition = player.transform.position;
        enemyPosition = gameObject.transform.position;

        Vector3 enemyScale = transform.localScale;

        if (player.transform.position.x < this.transform.position.x)
        {
            enemyScale.x = 1;
        }
        else
        {
            enemyScale.x = -1;
        }

        transform.localScale = enemyScale;
    }

    void checkForPlayer()
    {
        Destination = player.transform.position;
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
        else if (inRange == true && (Distance < .5))
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

        //StartCoroutine(unFreezePosition(1f));
        //Move enemy back
    }
}
