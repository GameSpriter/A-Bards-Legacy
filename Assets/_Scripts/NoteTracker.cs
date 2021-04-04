﻿using System.Collections;
using System.Collections.Generic;
<<<<<<< Updated upstream
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
    private string songSubstring = "";

    private const string swordSequence_c = "UUDUU";
    private const string bowSequence_c = "LLRLL";

    private string[] weaponSongs = { swordSequence_c, bowSequence_c };

    public MouseOnInputNote[] mc;

    // Update is called once per frame
    void Update()
    {
        if (!mc[0].IsHit && mc[0].GetInputNoteName == "Up Button")
        {
            song += "U";
            songSubstring += "U";
            //Debug.Log(song);
            Debug.Log(songSubstring + isValidSong());
        }
        else if (!mc[1].IsHit && mc[1].GetInputNoteName == "Right Button")
        {
            song += "R";
            songSubstring += "R";
            //Debug.Log(song);
            Debug.Log(songSubstring + isValidSong());
        }
        else if (!mc[2].IsHit && mc[2].GetInputNoteName == "Left Button")
        {
            song += "L";
            songSubstring += "L";
            //Debug.Log(song);
            Debug.Log(songSubstring + isValidSong());
        }
        else if (!mc[3].IsHit && mc[3].GetInputNoteName == "Down Button")
        {
            song += "D";
            songSubstring += "D";
            //Debug.Log(song);
            Debug.Log(songSubstring + isValidSong());
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
    /// Checks for a valid sequence
    /// </summary>
    /// <param name="sequence"></param>
    public void isValidSequence(string sequence)
    {
        if (sequence == songSubstring)
        {
            //Add to multiplier
            //Reset tempSequence
            songSubstring = "";
        }
    }
}


=======
using UnityEngine;

/// <summary>
/// Future implementation for note rules(see game-ideas)
/// </summary>
public class NoteTracker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
>>>>>>> Stashed changes