using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBall : Enemy
{
    public GameObject projectile;
    public float projectileSpeed = 10f;
    public float attackDelay = 2f;
    private float nextAttack = 0f;

    public override void Attack()
    {
        float step = 1f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Player.S.transform.position, step);

        if (Time.time > nextAttack)
        {
            Vector3 pos = Player.S.transform.position;
            Vector3 shootDirection = pos - transform.position;
            shootDirection = Vector3.Normalize(shootDirection);
            GameObject bulletInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            Rigidbody2D rigidBody = bulletInstance.GetComponent<Rigidbody2D>();
            rigidBody.velocity = new Vector2(shootDirection.x * projectileSpeed, shootDirection.y * projectileSpeed);
            nextAttack = Time.time + attackDelay;
        }
    }
}