using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// This brackeys tutorial helped me with some of the setup for the player controler
// https://www.youtube.com/watch?v=_QajrabyTJc
//
public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public TMP_Text healthText;
    public float health = 100f;
    public float armor = 100f;
    public float damage = 10f;


    public GameObject currentWeapon;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float speed = 12.0f;
    public float gravity = -9.81f * 2f;
    public float jumpHeight = 1.3f;


    Vector3 velocity;
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        SetHealth();
        
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 1.3f;
        } 
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 12.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    void SetHealth()
    {
        healthText.text = health.ToString(); 
    }
}
