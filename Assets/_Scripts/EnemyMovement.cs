using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyAI
{
    [SerializeField] Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       if (distanceFromPlayer(playerTransform.position.x, transform.position.x))
        {
            if (playerTransform.position.x < transform.position.x)
            {
                Debug.Log("Hi");
            }
            else
            {
                
            }
        }
    }
}
