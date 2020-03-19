using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Enemy")
        {
            //call the take damage method of the enemy
            Destroy(gameObject);
        }
    }
}
