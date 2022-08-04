using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    Animator animator;

    public  Transform target;

    public int damage;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", true);
    }

    public void TurnOffTrigger() {
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", false);
    }

    public void Dead()
    {
        TurnOffTrigger();
        animator.SetTrigger("Dead");
        Invoke("DestroyZombie", 1.5f);
    }

    public void DestroyZombie()
    {
        Destroy(gameObject);
    }

    float DistanceToPlayer()
    {
        return Vector3.Distance(target.position, transform.position);
    }

    public void AttackPlayer() {
        target.GetComponent<PlayerController>().TakeHit(damage);
    }

    // Update is called once per frame
    void Update()
    {
        if(DistanceToPlayer() < 3.5) {
            TurnOffTrigger();
            animator.SetBool("Attack", true);
        } else
        {
            TurnOffTrigger();
            animator.SetBool("Walk", true);
        }

    }
}
