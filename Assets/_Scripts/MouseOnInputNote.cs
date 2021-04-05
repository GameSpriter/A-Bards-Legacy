using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to give this note an identity in NoteSpawn
/// </summary>
public class MouseOnInputNote : MonoBehaviour
{
<<<<<<< HEAD
<<<<<<< HEAD
//<<<<<<< Updated upstream
=======
>>>>>>> 73c82c150380afca1e09ea407fb0f5db9c0a92af
=======
<<<<<<< Updated upstream
>>>>>>> parent of 73c82c1... Revert "Hakeem's push"
    public Sprite selectSprite;
    private Sprite spriteOriginal;

    public string GetInputNoteName { get; set; }
    public bool IsHit { get; set; } = false;

    private void Start()
    {
        spriteOriginal = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
<<<<<<< HEAD
<<<<<<< HEAD
//=======
=======
=======
>>>>>>> parent of 73c82c1... Revert "Hakeem's push"
    private string getInputNoteName;
    public bool isHit = false;

    public string GetInputNoteName { get => getInputNoteName; set => getInputNoteName = value; }
<<<<<<< HEAD
//>>>>>>> Stashed changes
=======
>>>>>>> 73c82c150380afca1e09ea407fb0f5db9c0a92af
=======
>>>>>>> Stashed changes
>>>>>>> parent of 73c82c1... Revert "Hakeem's push"

    private void OnMouseOver()
    {
        GetInputNoteName = gameObject.name;
<<<<<<< HEAD
<<<<<<< HEAD
//<<<<<<< Updated upstream
=======
>>>>>>> 73c82c150380afca1e09ea407fb0f5db9c0a92af
=======
<<<<<<< Updated upstream
>>>>>>> parent of 73c82c1... Revert "Hakeem's push"
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
<<<<<<< HEAD
//=======
//>>>>>>> Stashed changes
=======
>>>>>>> 73c82c150380afca1e09ea407fb0f5db9c0a92af
=======
=======
>>>>>>> Stashed changes
>>>>>>> parent of 73c82c1... Revert "Hakeem's push"
    }

    private void OnMouseExit()
    {
        GetInputNoteName = "";
<<<<<<< HEAD
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
=======
<<<<<<< Updated upstream
        IsHit = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteOriginal;
=======
        isHit = false;
>>>>>>> Stashed changes
>>>>>>> parent of 73c82c1... Revert "Hakeem's push"
    }
}
