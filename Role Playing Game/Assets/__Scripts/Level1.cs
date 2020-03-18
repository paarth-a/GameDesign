using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    //set in inspector
    public GameObject[] playerTypes = new GameObject[3];
    //set dynamically
    public Player.PlayerType type;

    void Start()
    {
        if(type == Player.PlayerType.ARCHER)
        {
            Instantiate(playerTypes[0]);
        }
        else if (type == Player.PlayerType.KNIGHT)
        {
            Instantiate(playerTypes[1]);
        }
        else if (type == Player.PlayerType.WIZARD)
        {
            Instantiate(playerTypes[2]);
        }
        else
        {
            Debug.Log("PlayerType is null");
        }

        //this instantiates the enemies
    }

    void Update()
    {
        
    }
}
