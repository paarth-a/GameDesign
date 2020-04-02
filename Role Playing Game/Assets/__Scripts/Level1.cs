using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{
    public GameObject[] playerTypes = new GameObject[3];
    public CinemachineVirtualCamera vcam;

    public GameObject slime;
    public GameObject fireball;
    


    //Starts the first level
    void Start()
    {
        GameObject player = Instantiate(playerTypes[Menu.playerChoice - 1]);
        vcam.m_Follow = player.transform;
        
        Instantiate(slime);
        Instantiate(fireball);
    }

}