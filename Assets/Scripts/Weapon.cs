using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : Item
{

    //Unique to a Weapon Item
    Animator anim;
    public GameObject weaponModel;
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
            weaponModel.GetComponent<Collider>().isTrigger = true; //Get collider from the weapon model, if we are currently pressing the attack button, deal damage
            anim.SetBool("attacking", true);
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            weaponModel.GetComponent<Collider>().isTrigger = false; //If not attacking, dont do damage.
            anim.SetBool("attacking", false);
        }
    }

}
