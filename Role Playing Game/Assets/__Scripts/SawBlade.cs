using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : Enemy
{
    private float timeUntilNextDirection = 0f;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public override void Attack()
    {
        float step = 1f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Player.S.transform.position, step);
    }

    public override void Move()
    {
        if (timeUntilNextDirection <= 0f)
        {
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            if(randomX < 0)
            {
                randomX = -1f;
            }
            else
            {
                randomX = 1f;
            }

            if (randomY < 0)
            {
                randomY = -1f;
            }
            else
            {
                randomY = 1f;
            }
            body.velocity = new Vector2(randomX, randomY);

            timeUntilNextDirection = 1f;
        }
        else
        {
            timeUntilNextDirection -= Time.deltaTime;
        }
    }
}