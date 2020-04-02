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
    public float energyDashCost = 10f;

    private float currentEnergy;
    private int healPotions = 0;
    private int energyPotions = 0;
    private Animator animator;
    protected bool isWalking;
    protected bool isShooting;
    protected bool isAttacking;
    protected bool isDead;

    public Slider healthBar;
    public Slider energyBar;
    public Slider levelBar;
    public Text healPotionsDisplay;
    public Text energyPotionsDisplay;
    public Text puzzleDisplay;
    public Text levelDisplay;

    public float mindamage = 1f;

    public float maxhealth;
    public float coins;
    public int puzzlePieces;
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
        coinDisplay = GameObject.FindGameObjectWithTag("CoinDisplay").GetComponent<Text>();
        puzzleDisplay = GameObject.FindGameObjectWithTag("PuzzleDisplay").GetComponent<Text>();
        healPotionsDisplay = GameObject.FindGameObjectWithTag("HealthDisplay").GetComponent<Text>();
        energyPotionsDisplay = GameObject.FindGameObjectWithTag("EnergyDisplay").GetComponent<Text>();
        levelDisplay = GameObject.FindGameObjectWithTag("LevelDisplay").GetComponent<Text>();
        levelBar = GameObject.FindGameObjectWithTag("LevelBar").GetComponent<Slider>();
        maxhealth = health;
        level = new Level(1, speed, health, defence, attack);

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
        animator = GetComponent<Animator>();
        SetLevel();
        SetCoins();
        SetHealth();
        SetEnergy();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            coins += 1;
            SetCoins();
        }
        else if (other.gameObject.tag == "Item")
        {
            Destroy(other.gameObject);
            if(other.gameObject.GetComponent<Potion>().potionType == Potion.PotionType.HEALTH)
            {
                healPotions++;
                SetHealth();
            }
            else
            {
                energyPotions++;
                SetEnergy();
            }
        }
        else if(other.gameObject.tag == "Puzzle")
        {
            Destroy(other.gameObject);
            puzzlePieces++;
            SetPuzzle();
        }else if(other.gameObject.tag == "Portal")
        {
            DontDestroyOnLoad(this);
            Player.S.transform.position = new Vector3(0f, 0f, 0f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(other.gameObject.tag == "Enemy" && isAttacking)
        {
            Destroy(other.gameObject);
        }
    }

    public void SetCoins()
    {
        coinDisplay.text = "Coins: " + coins.ToString();
    }

    public void SetPuzzle()
    {
        puzzleDisplay.text = "Puzzle Pieces: " + puzzlePieces.ToString();
    }

    public void SetHealth()
    {
        healPotionsDisplay.text = "Health Potions: " + healPotions.ToString();
    }

    public void SetEnergy()
    {
        energyPotionsDisplay.text = "Energy Potions: " + energyPotions.ToString();
    }

    public void SetLevel()
    {
        levelDisplay.text = "Level: " + level.level.ToString();
    }

    void FixedUpdate()
    {
        Move();

        RegainEnergy();

        healthBar.value = health / maxhealth;
        energyBar.value = currentEnergy / maxEnergy;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentEnergy > energyCost && !isAttacking)
        {
            isShooting = true;
            Attack();
            currentEnergy -= energyCost;
            Invoke("setShooting", 1f);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            isAttacking = true;
            Attacking();
            currentEnergy -= energyCost;
            Invoke("setAttacking", 1f);
        }
        if (Input.GetKeyDown(KeyCode.Q) && healPotions > 0)
        {
            healPotions--;
            if (health + Potion.value < maxhealth)
            {
                health += Potion.value;
            }
            else
            {
                health = maxhealth;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && energyPotions > 0)
        {
            energyPotions--;
            if (health + Potion.value < maxhealth)
            {
                currentEnergy += Potion.value;
            }
            else
            {
                currentEnergy = maxEnergy;
            }
        }

        SetAnimationState();

        levelBar.value = (float)level.currentExperience / (float)1000;
    }

    private void setAttacking()
    {
        isAttacking = false;
    }

    private void setShooting()
    {
        isShooting = false;
    }

    private void SetAnimationState()
    {
        if (isDead)
        {
            animator.SetBool("isDead", true);
        }
        else
        {
            animator.SetBool("isDead", false);
        }
        if (isShooting)
        {
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
        if (isAttacking)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
        if (isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    //Manages movement and dash (teleport)
    void Move()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        if(xAxis != 0 || yAxis != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        if (Input.GetKeyDown("space") && currentEnergy >= energyDashCost)
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
            currentEnergy -= energyDashCost;
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
            health -= mindamage;
        }
        if(health <= 0f)
        {
            isDead = true;
            Invoke("Reset", 3f);
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene("Menu");
        Destroy(gameObject);
    }

    public abstract void Attacking();

}