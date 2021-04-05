using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to give this note an identity in NoteSpawn
/// </summary>
public class MouseOnInputNote : MonoBehaviour
{
<<<<<<< HEAD
//<<<<<<< Updated upstream
=======
>>>>>>> 73c82c150380afca1e09ea407fb0f5db9c0a92af
    public Sprite selectSprite;
    private Sprite spriteOriginal;

    public string GetInputNoteName { get; set; }
    public bool IsHit { get; set; } = false;

    private void Start()
    {
        spriteOriginal = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
<<<<<<< HEAD
//=======
    private string getInputNoteName;
    public bool isHit = false;

    public string GetInputNoteName { get => getInputNoteName; set => getInputNoteName = value; }
//>>>>>>> Stashed changes
=======
>>>>>>> 73c82c150380afca1e09ea407fb0f5db9c0a92af

    private void OnMouseOver()
    {
        GetInputNoteName = gameObject.name;
<<<<<<< HEAD
//<<<<<<< Updated upstream
=======
>>>>>>> 73c82c150380afca1e09ea407fb0f5db9c0a92af
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
<<<<<<< HEAD
//=======
//>>>>>>> Stashed changes
=======
>>>>>>> 73c82c150380afca1e09ea407fb0f5db9c0a92af
    }

    private void OnMouseExit()
    {
        GetInputNoteName = "";
<<<<<<< HEAD
//<<<<<<< Updated upstream
        IsHit = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteOriginal;
//=======
        isHit = false;
//>>>>>>> Stashed changes
=======
        IsHit = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteOriginal;
>>>>>>> 73c82c150380afca1e09ea407fb0f5db9c0a92af
    }
}
