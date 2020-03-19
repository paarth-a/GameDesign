using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Player>().TakeDamage(FireBall.attack);
            Destroy(gameObject);
        }
    }
}
