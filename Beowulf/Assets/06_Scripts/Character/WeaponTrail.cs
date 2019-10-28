using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrail : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject Trail;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Weapon.active)
            Trail.SetActive(true);
        else
            Trail.SetActive(false);
    }
}
