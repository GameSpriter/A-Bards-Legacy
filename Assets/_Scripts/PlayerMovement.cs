using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerSpeed = 4f;

    public Rigidbody2D rb;
    public Animator anim;

    Vector2 movement;

    private SpriteRenderer spriteRenderer;
    public GameObject shortSwordHitbox;

    char lastKeyPressed;

    bool mouseButtonDown = false;
    bool leftClick = false;
    bool mouseClickForCoroutine = true;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shortSwordHitbox.SetActive(false);
    }

    IEnumerator DeactivateShortSwordHitbox(float seconds)
    {
        float counter = seconds;
        while (counter > 0f)
        {
            yield return new WaitForSeconds(.50f);
            counter--;
        }
        shortSwordHitbox.SetActive(false);
    }

    void Update()
    {
        Vector3 playerScale = transform.localScale;
        if (Input.GetMouseButtonDown(2))
        {
            mouseButtonDown = !mouseButtonDown;
        }

        if (mouseButtonDown)
        {
            anim.SetBool("harmonicsAnim", true);
        }
        else
        {
            anim.SetBool("harmonicsAnim", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            mouseClickForCoroutine = true;
        }

        if (!Input.GetMouseButtonDown(0))
        {
            mouseClickForCoroutine = false;
        }

        if (mouseClickForCoroutine)
        {
            shortSwordHitbox.SetActive(true);
            StartCoroutine(DeactivateShortSwordHitbox(.1f));
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.D))
        {
            playerScale.x = -1;
        }

        else if(Input.GetKeyDown(KeyCode.D) && !Input.GetKeyDown(KeyCode.A))
        {
            playerScale.x = 1;
        }

        if (Input.GetMouseButton(0))
        {
            anim.SetBool("attackAnim", true);
        }
        else
        {
            anim.SetBool("attackAnim", false);
        }
        transform.localScale = playerScale;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * playerSpeed * Time.fixedDeltaTime);
    }

    //Original movement code, keeping just in case something goes wrong with the note script
    
    /*
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float smoothTime = 0.5f;

    public Animator anim;
    
    char lastKeyPressed;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0, speed * Time.deltaTime * smoothTime, 0);
                
                anim.SetBool("walkUp", true);
                anim.SetBool("walkDown", false);
                lastKeyPressed = 'W';
                
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, -speed * Time.deltaTime * smoothTime, 0);

                anim.SetBool("walkUp", false);
                anim.SetBool("walkDown", true);
                lastKeyPressed = 'S';
               
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-speed * Time.deltaTime * smoothTime, 0, 0);
                lastKeyPressed = 'A';
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(speed * Time.deltaTime * smoothTime, 0, 0);
                lastKeyPressed = 'D';
            }
            if(lastKeyPressed == 'W')
            {
                anim.SetBool("walkUp", false);
                anim.SetBool("walkDown", false);
                anim.SetBool("idleUp", true);
                Debug.Log("Up");
            }

            if(lastKeyPressed == 'S')
            {
                anim.SetBool("walkUp", false);
                anim.SetBool("walkDown", false);
                anim.SetBool("idleDown", true);
                Debug.Log("Down");
            }
        }   
    }
    */
}
