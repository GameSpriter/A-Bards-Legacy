using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to give this note an identity in NoteSpawn
/// </summary>
public class MouseOnInputNote : MonoBehaviour
{
    public Sprite selectSprite;
    private Sprite spriteOriginal;

    public string GetInputNoteName { get; set; }
    public bool IsHit { get; set; } = false;

    private void Start()
    {
        spriteOriginal = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    //private void OnMouseOver()
    private void OnMouseEnter()
    {
        GetInputNoteName = gameObject.name;
        switch (gameObject.name)
        {
            case "Down Button":
                gameObject.GetComponent<SpriteRenderer>().sprite = selectSprite;
                break;
            case "Up Button":
                gameObject.GetComponent<SpriteRenderer>().sprite = selectSprite;
                break;
            case "Left Button":
                gameObject.GetComponent<SpriteRenderer>().sprite = selectSprite;
                break;
            case "Right Button":
                gameObject.GetComponent<SpriteRenderer>().sprite = selectSprite;
                break;
            default:
                break;
        }
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
        //gameObject.GetComponent<AudioSource>().Stop();
    }

    private void OnMouseExit()
    {
        GetInputNoteName = "";
        IsHit = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteOriginal;
    }
}
