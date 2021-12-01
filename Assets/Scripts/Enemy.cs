using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 100.0f;
    public float damange = 10.0f;
    public float lookRadius = 10f;
    public BoxCollider weapon;
    Animator anim;
    Transform target;
    NavMeshAgent agent;
    




    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            anim.SetBool("walking", true);


            if (distance <= agent.stoppingDistance)
            {
                
                // Attack target
                FaceTarget();
                weapon.isTrigger = true;
                anim.SetBool("attacking", true);

            }
            weapon.isTrigger = false;
            //anim.SetBool("attacking", false);
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Melee")
        {
            health -= 10;
            //Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
