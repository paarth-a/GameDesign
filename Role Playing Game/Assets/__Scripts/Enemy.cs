using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public static GameObject player;

    [Header("Set in Inspector")]
    public float speed = 10f;
    public float health = 10f;
    public float defence = 10f;
    public float attack = 10f;
    public float ranged = 10f;

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

    void LateStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    { 
        if((player.transform.position - transform.position).magnitude < 3f)
        {
            Attack();
        }
    }

    public abstract void Attack();

    public void TakeDamage(float damageAmount)
    {
        Debug.Log(damageAmount);
        if (damageAmount - defence > 0f)
        {
            health -= damageAmount - defence;
        }
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
