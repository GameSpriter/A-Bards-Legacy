using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
}
