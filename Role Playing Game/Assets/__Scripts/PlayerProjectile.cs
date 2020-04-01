using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Manages the player projectiles
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().level.IncreaseExperience(coll.gameObject.GetComponent<Enemy>().experience);
            Destroy(gameObject);
        }
    }
}
