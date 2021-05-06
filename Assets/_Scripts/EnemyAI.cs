using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    enum State
    {
        Idle,
        Chasing,
        Attacking,
    }

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

    public GameObject eighthNoteAttackHitbox;

    State state;

    IEnumerator DeactivateEighthNoteHitbox(float seconds)
    {
        float counter = seconds;
        while (counter > 0f)
        {
            yield return new WaitForSeconds(.50f);
            counter--;
        }
        //Debug.Log("Attacked!");
        eighthNoteAttackHitbox.SetActive(false);
    }

    private void Awake() {
        if(player == null) {
            player = Camera.main.GetComponent<CameraRefs>().player;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        eighthNoteAttackHitbox = transform.GetChild(0).gameObject;
        eighthNoteAttackHitbox.SetActive(false);

        state = State.Idle;
    }

    void Update()
    {
        //Debug.Log("Attack Cooldown: " + attackCooldown);

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
                transform.position = enemyPosition;
                break;
            case State.Chasing:
                chasePlayer();
                break;
            case State.Attacking:
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
        animator.Play("Chompy Boy_Attack_D");
        state = State.Idle;
        eighthNoteAttackHitbox.SetActive(true);
        attackCooldown = true;
        cooldownTime = 3f;
        StartCoroutine(DeactivateEighthNoteHitbox(.1f));

        //Move enemy back
    }
}
