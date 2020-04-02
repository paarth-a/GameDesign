using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    //Manages enemy damage
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Player>().TakeDamage(FireBall.attack);
            Destroy(gameObject);
        }else if (coll.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }
    }
}
