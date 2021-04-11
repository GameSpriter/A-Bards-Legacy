using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Set up for Elemental Effects
/// </summary>
public class Element : MonoBehaviour
{
    public virtual float ElementDamage { get; set; }
    public virtual float AreaOfEffect { get; set; }
    public virtual float RateOfElement { get; set; }
    public virtual float ElementLifeTime { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
