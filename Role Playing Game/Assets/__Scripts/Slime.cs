using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public static GameObject player;

    void Start()
    {
        Debug.Log("A");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }
}
