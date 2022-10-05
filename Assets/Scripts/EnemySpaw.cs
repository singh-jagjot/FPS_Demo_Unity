using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaw : MonoBehaviour
{
    [Header("Properties")]
    public GameObject enemy1;
    public GameObject enemy2;
    public Transform player;
    public int noOfEnemiesX2 = 20;

    public Canvas gameInfo;

    [Header("Spawn Range")]
    public int minX = -100;
    public int maxX = 100;
    public int minZ = -100;
    public int maxZ = 100;

    private int enemySpawnCount = 0;
    void Start()
    {
        for (int i = 0; i < this.noOfEnemiesX2; i++)
        {
            Vector3 randomPosition1 = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
            EnemyHandler enemy1Handler = Instantiate(enemy1, randomPosition1, Quaternion.identity).GetComponentInParent<EnemyHandler>();
            enemy1Handler.setId(++enemySpawnCount);
            enemy1Handler.player = player;
            enemy1Handler.setGameInfo(gameInfo);

            Vector3 randomPosition2 = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
            EnemyHandler enemy2Handler = Instantiate(enemy2, randomPosition2, Quaternion.identity).GetComponentInParent<EnemyHandler>();
            enemy2Handler.setId(++enemySpawnCount);
            enemy2Handler.player = player;
            enemy2Handler.setGameInfo(gameInfo);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
