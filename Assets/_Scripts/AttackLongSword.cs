using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLongSword : Attack
{
    
    // Start is called before the first frame update
    void Start()
    {
        AttackDamage = 5.0f;
        AttackRate = 5.0f;
        AttackLifeTime = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
