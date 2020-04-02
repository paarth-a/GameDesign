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
    public int coinDrops = 2;
    public float attackRadius = 3f;
    public GameObject healPotion;
    public GameObject energyPotion;

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
        if(Player.S != null && (Player.S.transform.position - transform.position).magnitude < attackRadius)
        {
            Attack();
        }
        else
        {
            Move();
        }
    }

    public abstract void Attack();

    public virtual void Move()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            coll.gameObject.GetComponent<Player>().TakeDamage(attack);
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
            InstantiatePotions();
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

    private void InstantiatePotions()
    {
        float random = Random.Range(0f, 2f);
        if(random < 1f)
        {
            Instantiate(healPotion, transform.position, Quaternion.identity);
        }
        else if(random < 2f)
        {
            Instantiate(energyPotion, transform.position, Quaternion.identity);
        }
    }
}
