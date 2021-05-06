using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    enum State
    {
        Idle,
        Chasing,
        Attacking,
    }

    [SerializeField] private bool inRange = false;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 playerPosition;
    [SerializeField] private Vector2 enemyPosition;
    [SerializeField] Vector2 Destination;
    [SerializeField] float Distance;
    [SerializeField] float chaseRange = 4f;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool attackCooldown = false;
    [SerializeField] private float cooldownTime = 3f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    State state;

    private void Awake() {
        if(player == null) {
            player = Camera.main.GetComponent<CameraRefs>().player;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        state = State.Idle;
    }

    void Update()
    {
        if (!attackCooldown)
            checkForPlayer();
        else
        {
            if (cooldownTime > 0f)
                cooldownTime -= Time.deltaTime;
            else
                attackCooldown = false;
        }

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

        switch(state)
        {
            default:
            case State.Idle:
                Debug.Log("Enemy Idle");
                transform.position = enemyPosition;
                break;
            case State.Chasing:
                Debug.Log("Enemy Chasing");
                chasePlayer();
                break;
            case State.Attacking:
                Debug.Log("Enemy Attacking");
                meleeAttackPlayer();
                break;
        }
    }

    void checkForPlayer()
    {
        Destination = player.transform.position;
        Distance = Vector2.Distance(gameObject.transform.position, Destination);

        if (Distance > chaseRange)
        {
            state = State.Idle;
        }
        else if (Distance < chaseRange && Distance > attackRange)
        {
            state = State.Chasing;
        }
        else if (Distance < attackRange)
        {
            state = State.Attacking;
        }

    }

    //void combatWithPlayer()
    //{
    //    if (inRange == true && (Distance > chaseRange))
    //    {
    //        transform.position = enemyPosition;
    //    }
    //    else if (inRange == true && (Distance < chaseRange && Distance > attackRange))
    //    {
    //        chasePlayer();
    //    }
    //    else if (inRange == true && (Distance < attackRange))
    //    {
    //        meleeAttackPlayer();
    //    }
    //}

    void chasePlayer()
    {
        float enemySpeed = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, enemySpeed);
    }

    void meleeAttackPlayer()
    {
        //Attack code here, along with damage and animation
        animator.Play("Chompy Boy_Attack_D");
        state = State.Idle;
        attackCooldown = true;
        cooldownTime = 3f;

        //Move enemy back
    }
}
