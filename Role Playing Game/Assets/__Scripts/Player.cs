using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Player : MonoBehaviour
{
    static public Player S;

    public float speed = 10f;
    public float health = 10f;
    public float defence = 10f;
    public float attack = 10f;
    public float ranged = 10f;
    public float dashCooldown = 2f;
    public float maxEnergy = 100f;
    public float energyCost = 10f;
    public float energyRegen = 10f;

    private float currentEnergy;
    private float nextDash = 0f;

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

    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Player.Awake() - Attempted to assign second Player.S!");
        }
    }

    void Start()
    {
        currentEnergy = maxEnergy;
    }

    void FixedUpdate()
    {
        Move();

        if (Input.GetMouseButtonDown(0) && currentEnergy > energyCost)
        {
            Attack();
            currentEnergy -= energyCost;
        }

        RegainEnergy();
    }

    void Move()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        if (Input.GetKeyDown("space") && Time.time > nextDash)
        {
            Dash(xAxis, yAxis);
        }
    }

    void Dash(float xAxis, float yAxis)
    {
        Vector3 pos = transform.position;
        pos.x += 3f * xAxis;
        pos.y += 3f * yAxis;
        if(Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(xAxis, yAxis), 3f).collider == null)
        {
            transform.position = pos;
            nextDash = Time.time + dashCooldown;
        }
    }

    public void RegainEnergy()
    {
        if(currentEnergy >= maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        else
        {
            currentEnergy += energyRegen * Time.deltaTime;
        }
    }

    public abstract void Attack();

    public void TakeDamage(float damageAmount)
    {
        if(damageAmount - defence > 0f)
        {
            health -= damageAmount - defence;
        }
        else
        {
            health -= 1f;
        }
        if(health <= 0f)
        {
            Invoke("Reset", 3f);
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene("Menu");
        Destroy(gameObject);
    }
}