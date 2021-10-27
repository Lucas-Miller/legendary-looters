using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : Item
{

    //Unique to a Weapon Item
    Animator anim;
    public GameObject model;
    public float damage = 10.0f;
    public float useSpeed = 10.0f;
    public float movementModifier = 0.0f;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GetComponent<Collider>().isTrigger = true;
            anim.SetBool("attacking", true);
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            GetComponent<Collider>().isTrigger = false;
            anim.SetBool("attacking", false);
        }
    }

}
