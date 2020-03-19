using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Player
{
    public GameObject projectile;
    public float projectileSpeed;

    public override void Attack()
    {
        Vector3 shootDirection = Input.mousePosition;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        shootDirection = Vector3.Normalize(shootDirection);
        GameObject bulletInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Rigidbody2D rigidBody = bulletInstance.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(shootDirection.x * projectileSpeed, shootDirection.y * projectileSpeed);
    }
}
