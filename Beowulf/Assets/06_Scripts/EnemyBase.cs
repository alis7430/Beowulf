using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public BaseCharacter stats;

    private float MaxHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = stats.HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
