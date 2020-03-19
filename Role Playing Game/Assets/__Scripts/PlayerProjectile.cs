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
            coll.gameObject.GetComponent<Enemy>().TakeDamage(Player.S.attack);
            Destroy(gameObject);
        }
    }
}
