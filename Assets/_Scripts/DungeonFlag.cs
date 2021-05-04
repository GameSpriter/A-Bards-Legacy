using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DungeonFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            SceneManager.LoadScene("DungeonTest", LoadSceneMode.Single);
        }
    }

}
