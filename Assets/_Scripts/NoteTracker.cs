using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Tracks all notes to conduct songs for weapons, elements, buffs, and specials
/// </summary>
public class NoteTracker : MonoBehaviour
{
    /*
     * iterate through string song
     * foreach char note in song add them to a temporary string tempSequence
     * check if tempSequence is the starting sub string of valid sequences in the List
     * check if tempSequence matches song in list(if it does Do something then reset temp)
     * if there is an invalid character in temporary string (Do something)
     */

    public string song = "";
    public int songlength = 0;
    private string songSubstring = "";

    private const string swordSequence_c = "UUDUU";
    private const string bowSequence_c = "LLRLL";

    private string[] weaponSongs = { swordSequence_c, bowSequence_c };

    public List<string> playedSequences = new List<string>();

    public MouseOnInputNote[] mc;

    public bool IsActivated { get; set; } = false;
    public Animator anim;
    public bool longSwordChange = false;
    public bool shortSwordChange = true;
    public bool bowChange = false;
    public PlayerMovement playerMovement;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (!mc[0].IsHit && mc[0].GetInputNoteName == "Up Button")
        {
            song += "U";
            songSubstring += "U";
            //Debug.Log(song);
            Debug.Log(songSubstring);
            //Debug.Log(songSubstring + isValidSong());
        }
        else if (!mc[1].IsHit && mc[1].GetInputNoteName == "Right Button")
        {
            song += "R";
            songSubstring += "R";
            //Debug.Log(song);
            Debug.Log(songSubstring);
            //Debug.Log(songSubstring + isValidSong());
        }
        else if (!mc[2].IsHit && mc[2].GetInputNoteName == "Left Button")
        {
            song += "L";
            songSubstring += "L";
            //Debug.Log(song);
            Debug.Log(songSubstring);
            //Debug.Log(songSubstring + isValidSong());
        }
        else if (!mc[3].IsHit && mc[3].GetInputNoteName == "Down Button")
        {
            song += "D";
            songSubstring += "D";
            //Debug.Log(song);
            Debug.Log(songSubstring);
            //Debug.Log(songSubstring + isValidSong());
        }
    }

    /// <summary>
    /// Tracks the whether an invalid note has been played or not.
    /// Also checks if a valid sequence has been played that adds to a certian effects multiplier.
    /// </summary>
    /// <returns>Returns true for no invalid notes played, else false</returns>
    public bool isValidSong()
    {
        foreach (string sequence in weaponSongs)
        {
            if (sequence.StartsWith(songSubstring))
            {
                isValidSequence(sequence);
                return true;
            }
        }

        songSubstring = "";

        return false;
    }

    /// <summary>
    /// Checks for a valid sequence that applies effects
    /// </summary>
    /// <param name="sequence"></param>
    public void isValidSequence(string sequence)
    {
        if (sequence == songSubstring)
        {
            //Add to multiplier
            playedSequences.Add(sequence);

            //Reset tempSequence
            songSubstring = "";
        }
    }

    /// <summary>
    /// Searches through song for sequences after harmonics closes
    /// </summary>
    public void SequenceSongSearch()
    {
        string tempSong = song;
        string sequenceSubstring = "";
        bool isValidNote = false;

        for (int i = 0; i < tempSong.Length; i++)
        {
            sequenceSubstring += tempSong[i];
            foreach (string weaponSong in weaponSongs)
            {
                if (weaponSong.StartsWith(sequenceSubstring))
                {
                    isValidNote = true;
                }
                if (weaponSong.Equals(sequenceSubstring))
                {
                    playedSequences.Add(sequenceSubstring);
                    sequenceSubstring = "";
                    break;
                }
            }
            if (!isValidNote)
            {
                sequenceSubstring = sequenceSubstring.Remove(0,1);
            }
            isValidNote = false;
        }
    }

    /*
    /// <summary>
    /// Matches up valid sequence with song in songeffect list
    /// </summary>
    private void SequenceMatchUp(string validSequence, string sequenceSubstring)
    {
        if (validSequence.Equals(sequenceSubstring))
        {
            playedSequences.Add(validSequence);
            sequenceSubstring = "";
        }
    }
    */

    /// <summary>
    /// Iterates through sequences played and matches them with valid effect sequences
    /// </summary>
    public void SequenceEffectMatch()
    {
        foreach (string sequence in playedSequences)
        {
            foreach (string effectSong in weaponSongs)
            {
                if(sequence == effectSong)
                {
                    //playerMovement.StopAllCoroutines();
                    ActivateEffect(sequence);
                }
            }
        }
        IsActivated = false;
    }

    /// <summary>
    /// When harmonics mode exits, sequence effects are applied
    /// </summary>
    public void ActivateEffect(string effect)
    {
        switch (effect)
        {
            case swordSequence_c:
                longSwordChange = true;
                shortSwordChange = false;
                bowChange = false;
                //Camera.main.GetComponent<CameraRefs>().player.GetComponent<PlayerMovement>().UpdateActiveWeapon();
                Camera.main.GetComponent<CameraRefs>().player.GetComponent<PlayerMovement>().StopAllCoroutines();
                break;
            case bowSequence_c:
                longSwordChange = false;
                shortSwordChange = false;
                bowChange = true;
                //Camera.main.GetComponent<CameraRefs>().player.GetComponent<PlayerMovement>().UpdateActiveWeapon();
                Camera.main.GetComponent<CameraRefs>().player.GetComponent<PlayerMovement>().StopAllCoroutines();
                break;
            default:
                break;
        }
    }
}
