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
    public GameObject coin;
    public int coinDrops;

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
            Player.S.level.IncreaseExperience(experience);
            InstantiateCoins();
            Destroy(gameObject);
        }
    }

    private void InstantiateCoins()
    {
        float radius = 0.5f;
        for (int i = 0; i < coinDrops; i++)
        {
            float angle = i * Mathf.PI * 2 / coinDrops;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

            Instantiate(coin, pos + transform.position, Quaternion.identity);
        }
    }
}
