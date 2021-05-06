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
    public GameObject longSwordHitbox;

    char lastKeyPressed;

    bool mouseButtonDown = false;
    bool mouseClickForCoroutine = true;
    bool inHarmonicsMode = false;
    bool longSwordActive;
    bool shortSwordActive;
    bool bowActive;

    public GameObject noteTracker;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shortSwordHitbox.SetActive(false);
        longSwordHitbox.SetActive(false);

        UpdateActiveWeapon();
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

    IEnumerator BackToSword(float seconds)
    {
        float counter = seconds;
        while (counter > 0f)
        {
            counter--;
            yield return new WaitForSeconds(.50f);
        }
        anim.SetBool("backToShortSword", true);
        anim.SetBool("shortSwordAnim", true);
        anim.SetBool("longSwordAnim", false);
        anim.SetBool("bowAnim", false);
        UpdateActiveWeapon();
        shortSwordActive = true;
    }

    public void UpdateActiveWeapon() {
        shortSwordActive = noteTracker.GetComponent<NoteTracker>().shortSwordChange;
        longSwordActive = noteTracker.GetComponent<NoteTracker>().longSwordChange;
        bowActive = noteTracker.GetComponent<NoteTracker>().bowChange;
    }

    void Update()
    {
        anim.SetBool("backToShortSword", false);

        Vector3 playerScale = transform.localScale;
        if (Input.GetMouseButtonDown(2))
        {
            mouseButtonDown = !mouseButtonDown;
        }

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
                StartCoroutine(BackToSword(6f));

            }
            else if (shortSwordActive == true)
            {
                anim.SetBool("shortSwordAnim", true);
                anim.SetBool("longSwordAnim", false);
                anim.SetBool("bowAnim", false);
                noteTracker.GetComponent<NoteTracker>().shortSwordChange = true;
                noteTracker.GetComponent<NoteTracker>().longSwordChange = false;
                noteTracker.GetComponent<NoteTracker>().bowChange = false;
            }
            else if (bowActive == true)
            {
                anim.SetBool("shortSwordAnim", false);
                anim.SetBool("longSwordAnim", false);
                anim.SetBool("bowAnim", true);
                noteTracker.GetComponent<NoteTracker>().shortSwordChange = false;
                noteTracker.GetComponent<NoteTracker>().longSwordChange = false;
                noteTracker.GetComponent<NoteTracker>().bowChange = true;
                StartCoroutine(BackToSword(10f));

            }
            inHarmonicsMode = false;
        }

        if (Input.GetMouseButtonDown(0) && inHarmonicsMode == false)
        {
            Debug.Log("Mouse button down");
            anim.SetBool("ifAttacking", true);
            mouseClickForCoroutine = true;
        }
        else
        {
            anim.SetBool("ifAttacking", false);
            mouseClickForCoroutine = false;
        }

        if (mouseClickForCoroutine)
        {
            Debug.Log("Start mouse click coroutine");
            //shortSwordActive = true;
            if (longSwordActive == true)
            {
                Debug.Log("long sword down");

                longSwordHitbox.SetActive(true);
                shortSwordActive = false;
                StartCoroutine(DeactivateLongSwordHitbox(.1f));
            }
            if (shortSwordActive == true)
            {
                Debug.Log("short sword down");

                shortSwordHitbox.SetActive(true);
                longSwordActive = false;
                StartCoroutine(DeactivateShortSwordHitbox(.1f));
            }
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
        transform.localScale = playerScale;

        noteTracker.GetComponent<NoteTracker>().shortSwordChange = false;
        noteTracker.GetComponent<NoteTracker>().longSwordChange = false;
        noteTracker.GetComponent<NoteTracker>().bowChange = false;
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
