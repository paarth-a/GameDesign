using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 10f;
    public float health = 10f;
    public float defence = 5f;
    public static float attack = 10f;
    public float ranged = 10f;
    public Level level = new Level();
    public int experience;

    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    void LateUpdate()
    { 
        if(Player.S != null && (Player.S.transform.position - transform.position).magnitude < 3f)
        {
            Attack();
        }
    }

    public abstract void Attack();

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Player>().TakeDamage(attack);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (damageAmount - defence > 0f)
        {
            health -= damageAmount - defence;
        }
        else
        {
            health -= 1f;
        }
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
