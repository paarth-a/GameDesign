using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Level2 : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;

    public GameObject slime;
    public GameObject saw;
    public GameObject eyeball;
    public GameObject puzzle;
    public GameObject portal;

    //Starts the first level
    void Start()
    {
        vcam.m_Follow = Player.S.transform;
        InvokeRepeating("SpawnEnemy", 0f, 1f);
    }

    private void SpawnEnemy()
    {
        Vector3 enemyPosition;
        GameObject enemy = null;
        do
        {
            float enemyType = Random.Range(0f, 3f);
            if (enemyType < 1f)
            {
                enemy = slime;
            }
            else if (enemyType >= 1f && enemyType < 2f)
            {
                enemy = saw;
            }
            else
            {
                enemy = eyeball;
            }
            float randomX = Random.Range(5f, 10f);
            float randomY = Random.Range(5f, 10f);
            float multiplierX = Random.Range(-1f, 1f);
            float multiplierY = Random.Range(-1f, 1f);
            if (multiplierX < 0f)
            {
                randomX *= -1;
            }
            if (multiplierY < 0f)
            {
                randomY *= -1;
            }
            enemyPosition = new Vector3(randomX, randomY, 0f);
            enemyPosition += Player.S.transform.position;
        } while (Physics2D.OverlapPoint(enemyPosition));

        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length < 15)
        {
            Instantiate(enemy, enemyPosition, Quaternion.identity);
        }
    }
}