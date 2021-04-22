using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] enemyPrefabs;
    public int[] spawnCounts;
    public float[] spawnIntervals;
    public int maxEnemies;
}

//[System.Serializable]
//public class Subwave
//{
//    public GameObject[] enemyPrefabs;
//    public int[] spawnCounts;
//    public float[] spawnIntervals;
//    public int maxEnemies;
//}

public class SpawnEnemy : MonoBehaviour
{
    public Wave[] waves;
    public int timeBetweenWaves = 5;

    private GameManagerBehavior gameManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;
    private int subwaveEnemiesSpawned = 0;
    private int subwaveCount = 0;
    public GameObject[] livingEnemies;
    public bool GO;

    public GameObject[] waypoints;
    public GameObject[] rightWaypoints;
    public GameObject testEnemyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager =
            GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }


    // Update is called once per frame
    void Update()
    {
        // for each enemy that you spawn, consider the example:
        // maxEnemies = 16
        // enemyPrefabs = [enemy1,enemy1,enemy2,enemy1,enemy3]
        // spawnCounts[]=[3,3,4,5,1]
        // spawnIntervals=[2,1,1,1.5,10]
        // spawnCount=i (allow it to be 1)
        // for this wave, you will spawn 5 subwaves of enemies
        // the number of enemyPrefabs and spawnCounts/intervals all have to be 5
        // the subWave count determines which subwave we are on



        livingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GO = (livingEnemies.Length == 0);

        // 1 check to see if you're on the last wave
        int currentWave = gameManager.Wave;
        if (currentWave < waves.Length)
        {

            // 2 check if it is time to spawn an enemy based on the subwave spawnInterval
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnIntervals[subwaveCount];
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                 timeInterval > spawnInterval) &&
                enemiesSpawned < waves[currentWave].maxEnemies)
            {
                // 3 spawn the type of enemy specified by the subwave
                lastSpawnTime = Time.time;
                GameObject newEnemy = (GameObject)
                    Instantiate(waves[currentWave].enemyPrefabs[subwaveCount]);
                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
                enemiesSpawned++;
                subwaveEnemiesSpawned++;

                // if you've finished spawning all the enemies for the subwave,
                // then reset the subwave enemy count and go to next subwave
                if (subwaveEnemiesSpawned == waves[currentWave].spawnCounts[subwaveCount])
                {
                    subwaveEnemiesSpawned = 0;
                    subwaveCount++;
                    if (enemiesSpawned == waves[currentWave].maxEnemies)
                    {
                        subwaveCount--;
                    }
                }

            }
            // else if(((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
            //      timeInterval > spawnInterval) &&
            //     enemiesSpawned < waves[currentWave].maxEnemies && (currentWave==1 || currentWave%3==0)){

            //     lastSpawnTime = Time.time;
            //     GameObject newEnemy = (GameObject)
            //         Instantiate(waves[currentWave].enemyPrefab);
            //     newEnemy.GetComponent<MoveEnemy>().waypoints = rightWaypoints;
            //     enemiesSpawned++;
            //     }

            //print(enemiesSpawned == waves[currentWave].maxEnemies && GO);

            // 4 once the max enemies are spawned and there are no living enemies, end the wave
            if (enemiesSpawned == waves[currentWave].maxEnemies
                && GO
                )
            // && numAlive<=1
            {
                gameManager.Gold += 100+currentWave*30;
                gameManager.Wave++;

                enemiesSpawned = 0;
                subwaveCount = 0;
               subwaveEnemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
            // 5
        }
        else
        {
            gameManager.gameOver = true;
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }

    }
}
