using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Player
{
    public GameObject projectile;
    public float projectileSpeed = 5;

    //Manages shooting attack
    public override void Attack()
    {
        Vector3 shootDirection = Input.mousePosition;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        shootDirection.z = 0;
        Vector3 direction = shootDirection.normalized;
        GameObject bulletInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Rigidbody2D rigidBody = bulletInstance.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(direction.x * projectileSpeed, direction.y * projectileSpeed);
    }

    //Manages mele attack
    public override void Attacking()
    {

        float tempdefence = this.defence;
        this.defence = 1000;
        mindamage = 0f;
        float duration = 1.0f;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
        }

        this.defence = tempdefence;
        mindamage = 1f;
    }
}
