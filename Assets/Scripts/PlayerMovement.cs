using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


// This brackeys tutorial helped me with some of the setup for the player controler
// https://www.youtube.com/watch?v=_QajrabyTJc
//
public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public TMP_Text healthText;
    public TMP_Text scoreText;
    public int score;
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
    public bool hasHealthPotion = true;

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

        if (health <= 0)
        {
            SceneManager.LoadScene(0);
        }

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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(hasHealthPotion == true)
            {
                if(health < 100f)
                {
                    health += 20f;
                    hasHealthPotion = false;
                    if (health > 100f)
                        health = 100f;
                } 
            }    
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
        scoreText.text = score.ToString();
    }



    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "healthPotion")
        {
            if(hasHealthPotion == false)
            {
                hasHealthPotion = true;
                Destroy(other.gameObject);
            }
        }

        if(other.gameObject.tag == "bag")
        {
            Destroy(other.gameObject);
            score += Random.RandomRange(1, 1000);
            SetHealth();

        }

        if (other.gameObject.tag == "EnemyMelee")
        {
            health -= 30;
            SetHealth();
        }



    }
}
