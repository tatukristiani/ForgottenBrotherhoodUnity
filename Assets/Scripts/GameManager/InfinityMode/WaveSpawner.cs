using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*This script handles the spawning of waves/levels in infinity mode.*/
/*Example code taken from: https://www.youtube.com/watch?v=q0SBfDFn2Bs&ab_channel=Brackeys */
public class WaveSpawner : MonoBehaviour
{

    //SpawnState tells us what is happening right now.
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    //Wave class inside, just to simplify..
    [System.Serializable]
    public class Wave
    {
        public int level = 1;
        public Transform enemyPrefab;
        public int enemyCount;
        public float spawnRate;
    }

    //List of Wave objects used so adding waves is possible in code.
    //First wave added publicly in inspector though.
    public List<Wave> waves = new List<Wave>();
    public static int fixedWaveNumber = 0; //Just a quick fix so that displaying the round lasted on game over was possible.
    public int waveNumber = 0; 


    public Transform[] spawnPoints;

    private float timeBetweenWaves = 5f;
    private float waveCountdown; 
    private float enemyAliveCountdown;

    private Text levelText;





    private SpawnState state = SpawnState.COUNTING;

    //Checks if there is no spawnpoints and give error if so.
    //Sets the wavecountdown and level text.
    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawnpoints.");
        }

        waveCountdown = timeBetweenWaves;
        levelText = GameObject.Find("LevelNumber").GetComponent<Text>();
        levelText.text = "Level 1";

    }
     

    void Update()
    {
        //When all enemies have been spawned on current wave, the state is WAITING.
        if(state == SpawnState.WAITING)
        {
            //CHECK IF ENEMIES ARE STILL ALIVE
            if (!EnemyIsAlive())
            {
                //If there are no enemies the wave has been completed.
                WaveCompleted();
                levelText.text = "Level " + (waveNumber + 1);
           
                
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
                StartCoroutine(SpawnWave(waves[waveNumber]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }


    //Check if any enemies are still alive.
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


    //After wave has been completed, adds +1 to waveNumber and start countdown for next wave.
    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        
        Debug.Log("Next wave starting!");
        
        waveNumber++;
        fixedWaveNumber++;

        Debug.Log(fixedWaveNumber + "in spawner");
    }

    //SPAWNS ENEMIES FROM THE WAVE LIST WITH A CERTAIN SPAWNRATE. IEnumerator used so it is possible to have a delay in spawning the enemies.
    IEnumerator SpawnWave(Wave wave)
    {
       
        Debug.Log("Spawning wave" + wave.level);
        state = SpawnState.SPAWNING;
        
        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        state = SpawnState.WAITING;

        //after all enemies have been spawned we create a new wave beforehand using the last levels info. (level + 1, enemies + 3 every wave, everything else stays the same)
        waves.Add(new Wave { level = wave.level + 1, enemyPrefab = wave.enemyPrefab, enemyCount = wave.enemyCount += 3, spawnRate = wave.spawnRate });


        Debug.Log("Spawned all enemies.");
        yield break;
    }

    //This method spawns the enemy randomly to one of the 8 spawnpoints.
    void SpawnEnemy(Transform enemy)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, spawnPoint.position,spawnPoint.rotation);
        Debug.Log("Spawning enemy");
    }


}
