using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Player
{
    public GameObject projectile;
    public float projectileSpeed;

    //Shooting Attack
    public override void Attack()
    {
        Vector3 shootDirection = Input.mousePosition;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        shootDirection.z = 0;
        shootDirection = Vector3.Normalize(shootDirection);
        GameObject bulletInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Rigidbody2D rigidBody = bulletInstance.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(shootDirection.x * projectileSpeed, shootDirection.y * projectileSpeed);
    }

    //Mele Attack
    public override void Attacking()
    {
 
        float tempdefence = this.defence;
        this.defence = 1000;
        mindamage = 0f;
        float duration = 1.0f;
        float time = 0f;
        Collision2D coll = new Collision2D();

        do
        {
            time += Time.deltaTime;
            if (coll.gameObject.tag == "Enemy")
            {
                coll.gameObject.GetComponent<Enemy>().TakeDamage(Player.S.attack);
            }
        } while (time < duration);
        

        this.defence = tempdefence;
        mindamage = 1f;
    }
}
