using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    private void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public GameObject spawnPrefab;
   

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject, 2f);
    }

    public static void KillEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        
    }
}
