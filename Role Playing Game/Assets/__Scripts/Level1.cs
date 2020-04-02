﻿using Cinemachine;
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
        InvokeRepeating("SpawnEnemy", 0f, 1f);
    }

    private void SpawnEnemy()
    {
        Vector3 enemyPosition;
        GameObject enemy = null;
        do
        {
            float enemyType = Random.Range(0f, 2f);
            if (enemyType < 1f)
            {
                enemy = slime;
            }
            else if (enemyType >= 1f)
            {
                enemy = fireball;
            }
            float randomX = Random.Range(5f, 10f);
            float randomY = Random.Range(5f, 10f);
            float multiplierX = Random.Range(-1f, 1f);
            float multiplierY = Random.Range(-1f, 1f);
            if(multiplierX < 0f)
            {
                randomX *= -1;
            }
            if(multiplierY < 0f)
            {
                randomY *= -1;
            }
            enemyPosition = new Vector3(randomX, randomY, 0f);
            enemyPosition += Player.S.transform.position;
        } while (Physics2D.OverlapPoint(enemyPosition));

        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length < 10)
        {
            Instantiate(enemy, enemyPosition, Quaternion.identity);
        }
    }
}