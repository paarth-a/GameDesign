using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Manages the player projectiles
public class PlayerProjectile : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<Enemy>().TakeDamage(Player.S.attack);
            Destroy(gameObject);
        }
        else if (coll.gameObject.tag == "Walls")
            Destroy(gameObject);
    }
}
