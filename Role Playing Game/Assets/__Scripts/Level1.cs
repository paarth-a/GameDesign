using Cinemachine;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public GameObject[] playerTypes = new GameObject[3];
    public CinemachineVirtualCamera vcam;

    void Start()
    {
        GameObject player = Instantiate(playerTypes[Menu.playerChoice - 1]);
        vcam.m_Follow = player.transform;
    }
}