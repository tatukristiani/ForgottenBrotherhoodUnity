using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float spawnRate;
     
    }

    public Wave[] waves;
    private int nextWave = 0;
    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    private float enemyAliveCountdown;
    

    private SpawnState state = SpawnState.COUNTING;
    void Start()
    {
        waveCountdown = timeBetweenWaves;
       
    }

    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            //CHECK IF ENEMIES ARE STILL ALIVE
            if (!EnemyIsAlive())
            {
                //start new wave
                WaveCompleted();
            }
            else
            {
                return;
            }

        }
        if(waveCountdown <= 0)
        {
            //IF NOT SPAWNING START SPAWNING
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }
    bool EnemyIsAlive()
    {
        enemyAliveCountdown -= Time.deltaTime;
        if (enemyAliveCountdown <= 0f)
        {
            enemyAliveCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            } 
        }
        return true;
    }

    void WaveCompleted()
    {
        Debug.Log("Wave done");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("Waves completed Looping...");
        }
        else
        {
            nextWave++;
        }  
    }

    //SPAWNS ENEMIES FROM ARRAY WITH A CERTAIN SPAWNRATE
    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning wave" + wave.name);
        state = SpawnState.SPAWNING;

        for(int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        //spawn enemy
        Instantiate(enemy, transform.position, transform.rotation);
        Debug.Log("Spawning enemy" + enemy.name);
    }
}
