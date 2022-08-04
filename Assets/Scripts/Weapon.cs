using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform shotDirection;

    RaycastHit hitInfo;

    public AudioSource weapon;

    public AudioClip fireSE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            Fire();
        }
    }


    void Fire() {

        if(!weapon.isPlaying)
        {
        weapon.clip = fireSE;
        weapon.Play();
        }

        Debug.DrawRay(shotDirection.transform.position, shotDirection.transform.forward * 10, Color.blue);

        if (Physics.Raycast(shotDirection.transform.position, shotDirection.transform.forward, out hitInfo, 50))
        {
            Debug.Log("弾が「" + hitInfo.collider.gameObject.name + "」にヒットしました。");

            if( hitInfo.collider.gameObject.tag == "Zombie")
            {
                hitInfo.collider.gameObject.GetComponent<ZombieController>().Dead();
            }

            }

    }
}
