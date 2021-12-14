using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource backGroundMusic;
    void Start()
    {
        backGroundMusic.Play();
    }
}
