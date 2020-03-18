using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static public Player S;

    public float speed = 10f;
    public float health = 10f;
    public float defence = 10f;
    public float attack = 10f;
    public float ranged = 10f;
    public float dashCooldown = 2f;
    public PlayerType playerType { set; get; }

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

    // Start is called before the first frame update
    void Start()
    {
        S.playerType = Menu.playerType;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
        nextDash = Time.time + dashCooldown;
        Vector3 pos = transform.position;
        pos.x += 20f * xAxis;
        pos.y += 20f * yAxis;
        transform.position = pos;
    }

    public enum PlayerType
    {
        ARCHER, KNIGHT, WIZARD
    }
}
