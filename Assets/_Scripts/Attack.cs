using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the attributes and functionality of attacks
/// </summary>
public class Attack : MonoBehaviour
{
    public virtual float AttackDamage { get; set; }
    public virtual float AttackRate { get; set; }
    public virtual float AttackLifeTime { get; set; }
    public virtual float AttackElement { get; set; }
    public virtual float AttackBuff { get; set; }

    public Attack()
    {

    }

    void AttackAnimation() 
    {

    }
}
