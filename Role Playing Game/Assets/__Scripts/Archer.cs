using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Player
{
    public GameObject projectile;
    public float projectileSpeed = 25;

    public override void Attack()
    {
        Vector3 shootDirection = Input.mousePosition;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        Vector3 direction = shootDirection.normalized;
        GameObject bulletInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Rigidbody2D rigidBody = bulletInstance.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(direction.x * projectileSpeed, direction.y * projectileSpeed);
    }
}
