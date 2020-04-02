using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private List<GameObject> healPotions = new List<GameObject>();
    private List<GameObject> energyPotions = new List<GameObject>();

    public GameObject slime;
    public GameObject fireball;
    public Slider healthBar;
    public Slider energyBar;

    public float mindamage = 1f;

    public float maxhealth;
    public float coins;
    public Text coinDisplay;
    public Level level;

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
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        energyBar = GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<Slider>();
        maxhealth = health;
        level = new Level();

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
        InvokeRepeating("SpawnEnemy", 4f, 1f);
    }

    void OnTrigger(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coins += 1;
            SetCoins();
        }
        if (other.gameObject.CompareTag("Item"))
        {
            Destroy(other.gameObject);
            if(other.gameObject.GetComponent<Potion>().potionType == Potion.PotionType.HEALTH)
            {
                healPotions.Add(other.gameObject);
            }
            else
            {
                energyPotions.Add(other.gameObject);
            }
        }
    }

    public void SetCoins()
    {
        coinDisplay.text = "Coins: " + coins.ToString();
    }

    void FixedUpdate()
    {
        Move();

        if (Input.GetMouseButtonDown(0) && currentEnergy > energyCost)
        {
            Attack();
            currentEnergy -= energyCost;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Attacking();
            currentEnergy -= energyCost;
        }

        RegainEnergy();

        healthBar.value = health / maxhealth;
        energyBar.value = currentEnergy / maxEnergy;
    }

    //Manages movement and dash (teleport)
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

    //Makes sure teleportation doesn't end it a wall
    void Dash(float xAxis, float yAxis)
    {
        Vector3 pos = transform.position;
        pos.x += 3f * xAxis;
        pos.y += 3f * yAxis;
        if (!Physics2D.OverlapPoint(pos))
        {
            transform.position = pos;
            nextDash = Time.time + dashCooldown;
        }
    }

    //Replenishes energy
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

    //Adds damage to the player
    public void TakeDamage(float damageAmount)
    {
        if(damageAmount - defence > mindamage)
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

    //generates enemies around the player
    public void SpawnEnemy()
    {
        GameObject enemy = slime;
        float rand = Random.Range(0f, 1f);
        if (rand <= 0.5f)
        {
            enemy = slime;
        }
        if (rand > 0.5f)
        {
            enemy = fireball;
        }
        
        Vector3 pos = transform.position;
        float randx = Random.Range(0f, 1f);
        float multiplierx = 0f;
        if(randx <= 0.5f)
        {
            multiplierx = -1f;
        }
        if(randx > 0.5f)
        {
            multiplierx = 1f;
        }
        float randy = Random.Range(0f, 1f);
        float multipliery = 0f;
        if (randy <= 0.5f)
        {
            multipliery = -1f;
        }
        if (randy > 0.5f)
        {
            multipliery = 1f;
        }
        float newX = pos.x + (4f * multiplierx);
        float newY = pos.y + (4f * multipliery);
        float newZ = pos.z;
        Vector3 enemypos = transform.position;
        enemypos.Set(newX, newY, newZ);
        if (!Physics2D.OverlapPoint(enemypos))
        {
            Instantiate(enemy, enemypos, Quaternion.identity);
        }
        
    }

    public abstract void Attacking();

}