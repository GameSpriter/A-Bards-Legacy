using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public Animator anim;

    Vector2 movement;

    private SpriteRenderer spriteRenderer;

    public GameObject shortSwordHitbox;
    public GameObject longSwordHitbox;
    public GameObject noteTracker;
    public GameObject playerHitbox;

    bool mouseButtonDown = false;
    bool mouseClickForCoroutine = true;
    bool inHarmonicsMode = false;
    bool longSwordActive;
    bool shortSwordActive;
    bool bowActive;

    private string lastWeaponUsed;

    //NoteTracker noteTracker;
    public Transform shotSpawn;
    public GameObject arrowPrefab;

    float speed = 8f; 
    public float playerSpeed = 4f;
    public float playerDashSpeed = 50f;

    int dashTimer;

    Vector3 playerScale;
    private Vector2 positionAfterLeftDash;
    private Vector2 positionAfterRightDash;
    private Vector2 positionAfterDownDash;
    private Vector2 positionAfterUpDash;

    void Start()
    {
        //Getting components and setting active hitboxes to false 
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        shortSwordHitbox.SetActive(false);
        longSwordHitbox.SetActive(false);

        //Getting components from the NoteTracker script 
        longSwordActive = noteTracker.GetComponent<NoteTracker>().longSwordChange;
        shortSwordActive = noteTracker.GetComponent<NoteTracker>().shortSwordChange;
        bowActive = noteTracker.GetComponent<NoteTracker>().bowChange;

        playerScale = transform.localScale;
    }

    //Creates a timer that deactivates the hitbox of the short sword
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

    //Creates a timer that deactives the hitbox of the long sword
    IEnumerator DeactivateLongSwordHitbox(float seconds)
    {
        float counter = seconds;
        while (counter > 0f)
        {
            yield return new WaitForSeconds(.50f);
            counter--;
        }
        longSwordHitbox.SetActive(false);
    }
    
    //Creates a timer that disables all of the weapons except for the short sword
    IEnumerator BackToSword(float seconds)
    {
        float counter = seconds;
        while (counter > 0f)
        {
            counter--;
            yield return new WaitForSeconds(1f);
        }
        anim.SetBool("backToShortSword", true);
        anim.SetBool("shortSwordAnim", true);
        anim.SetBool("longSwordAnim", false);
        anim.SetBool("bowAnim", false);
        noteTracker.GetComponent<NoteTracker>().shortSwordChange = true;
        noteTracker.GetComponent<NoteTracker>().longSwordChange = false;
        noteTracker.GetComponent<NoteTracker>().bowChange = false;
        shortSwordActive = true;
    }

    //Stops the dash animation and then based on the last weapon the player had the proper weapon will be brought back out 
    IEnumerator stopDashAnimation(float seconds)
    {
        float counter = seconds;
        while (counter > 0f)
        {
            counter--;
            yield return new WaitForSeconds(.0001f);
        }
    
        if (lastWeaponUsed == "Short sword")
        {
            anim.SetBool("shortSwordAnim", true);
            anim.SetBool("longSwordAnim", false);
            anim.SetBool("bowAnim", false);
        }
        if (lastWeaponUsed == "Long sword")
        {
            anim.SetBool("shortSwordAnim", false);
            anim.SetBool("longSwordAnim", true);
            anim.SetBool("bowAnim", false);
        }
        if (lastWeaponUsed == "Bow")
        {
            anim.SetBool("shortSwordAnim", false);
            anim.SetBool("longSwordAnim", false);
            anim.SetBool("bowAnim", true);        
        }
        playerHitbox.SetActive(true);
    }

    void Update()
    {
        //Gets components
        shortSwordActive = noteTracker.GetComponent<NoteTracker>().shortSwordChange;
        longSwordActive = noteTracker.GetComponent<NoteTracker>().longSwordChange;
        bowActive = noteTracker.GetComponent<NoteTracker>().bowChange;

        anim.SetBool("backToShortSword", false);

        if (Input.GetMouseButtonDown(2))
        {
            mouseButtonDown = !mouseButtonDown;
        }

        //Sets harmonics system to true if the middle mouse button is clicked, otherwise runs weapon specific code 
        if (mouseButtonDown)
        {
            anim.SetBool("harmonicsAnim", true);
            anim.SetBool("shortSwordAnim", false);
            anim.SetBool("longSwordAnim", false);
            anim.SetBool("bowAnim", false);
            inHarmonicsMode = true;
        }
        else
        {
            anim.SetBool("harmonicsAnim", false);
            if (longSwordActive == true)
            {
                anim.SetBool("shortSwordAnim", false);
                anim.SetBool("longSwordAnim", true);
                anim.SetBool("bowAnim", false);
                noteTracker.GetComponent<NoteTracker>().shortSwordChange = false;
                noteTracker.GetComponent<NoteTracker>().longSwordChange = true;
                noteTracker.GetComponent<NoteTracker>().bowChange = false;
                StartCoroutine(BackToSword(10f));
                lastWeaponUsed = "Long sword";
            }
            else if (shortSwordActive == true)
            {
                anim.SetBool("shortSwordAnim", true);
                anim.SetBool("longSwordAnim", false);
                anim.SetBool("bowAnim", false);
                noteTracker.GetComponent<NoteTracker>().shortSwordChange = true;
                noteTracker.GetComponent<NoteTracker>().longSwordChange = false;
                noteTracker.GetComponent<NoteTracker>().bowChange = false;
                lastWeaponUsed = "Short sword";
            }
            else if (bowActive == true)
            {
                anim.SetBool("shortSwordAnim", false);
                anim.SetBool("longSwordAnim", false);
                anim.SetBool("bowAnim", true);
                noteTracker.GetComponent<NoteTracker>().shortSwordChange = false;
                noteTracker.GetComponent<NoteTracker>().longSwordChange = false;
                noteTracker.GetComponent<NoteTracker>().bowChange = true;
                StartCoroutine(BackToSword(30f));
                lastWeaponUsed = "Bow";
            }
            inHarmonicsMode = false;

            //noteTracker.GetComponent<NoteTracker>().shortSwordChange = false;
            //noteTracker.GetComponent<NoteTracker>().longSwordChange = false;
            //noteTracker.GetComponent<NoteTracker>().bowChange = false;
        }

        //Checking to see if the attack button is pressed and the player is not in harmonics 
        if (Input.GetMouseButtonDown(0) && inHarmonicsMode == false)
        {
            anim.SetBool("ifAttacking", true);
            mouseClickForCoroutine = true;
        }
        else
        {
            anim.SetBool("ifAttacking", false);
            mouseClickForCoroutine = false;
        }
        
        //If the mouse has been clicked to attack, depending on the weapon selected a different timer will be run to return the weapon back to the short sword
        //This part is most likely the buggiest area and probably needs the most refinement
        if (mouseClickForCoroutine)
        {
            //shortSwordActive = true;
            if (longSwordActive == true)
            {
                longSwordHitbox.SetActive(true);
                shortSwordActive = false;
                bowActive = false;
                StartCoroutine(DeactivateLongSwordHitbox(.1f));
            }
            if (shortSwordActive == true)
            {
                shortSwordHitbox.SetActive(true);
                longSwordActive = false;
                bowActive = false;
                StartCoroutine(DeactivateShortSwordHitbox(.1f));
            }
            if (bowActive == true)
            {
                FireBow();
            }
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        //Changing the direction of the player to turn sprites around 
        if (Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.D))
        {
            playerScale.x = -1;
        }

        else if(Input.GetKeyDown(KeyCode.D) && !Input.GetKeyDown(KeyCode.A))
        {
            playerScale.x = 1;

        }
        transform.localScale = playerScale;

        //Dash code, we need a way to lock the player out of spamming the dash through locking the use of the shift key out for a certain amount of time.
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerHitbox.SetActive(false);
                anim.SetBool("dashing", true);
                StartCoroutine(stopDashAnimation(.001f));
                dashTimer = 5;
                positionAfterLeftDash = Vector2.left * 1000;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerHitbox.SetActive(false);
                anim.SetBool("dashing", true);
                StartCoroutine(stopDashAnimation(.001f));
                dashTimer = 5;
                positionAfterRightDash = Vector2.right * 1000;
            }
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerHitbox.SetActive(false);
                anim.SetBool("dashing", true);
                StartCoroutine(stopDashAnimation(.001f));
                dashTimer = 5;
                positionAfterUpDash = Vector2.up * 1000;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerHitbox.SetActive(false);
                anim.SetBool("dashing", true);
                StartCoroutine(stopDashAnimation(.001f));
                dashTimer = 5;
                positionAfterDownDash = Vector2.down * 1000;
            }
        }
    }


    void FireBow()
    {
        GameObject arrowRBGameObject = Instantiate(arrowPrefab, shotSpawn.position, shotSpawn.rotation);
        
        if (playerScale.x == -1)
        {
            arrowRBGameObject.GetComponent<Rigidbody2D>().velocity = -transform.right * speed;
            arrowRBGameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            arrowRBGameObject.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
            arrowRBGameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        
        Destroy(arrowRBGameObject, 2f);
    }

    //Code that helps smooth out dash
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * playerSpeed * Time.fixedDeltaTime);

        if (dashTimer > 0)
        {
            rb.AddForce(positionAfterLeftDash, ForceMode2D.Force);
            rb.AddForce(positionAfterRightDash, ForceMode2D.Force);
            rb.AddForce(positionAfterUpDash, ForceMode2D.Force);
            rb.AddForce(positionAfterDownDash, ForceMode2D.Force);

            dashTimer--;
        }
        else if (dashTimer == 0)
        {
            positionAfterLeftDash = Vector2.zero;
            positionAfterRightDash = Vector2.zero;
            positionAfterUpDash = Vector2.zero;
            positionAfterDownDash = Vector2.zero;

            dashTimer--;
        }
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
