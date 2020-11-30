using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    [SerializeField]
    private GameObject gameOverUI;

    private void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }

        gameOverUI.SetActive(false);

    }

    //public Transform enemyPrefab;
    //public Transform spawnPoint;
    //public float spawnDelay = 2;
    //public GameObject spawnPrefab;


    //Destroys player gameobject after 2seconds and call GameOver() method.
    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject, 2f);
        gm.GameOver();
    }
    
    //Destroys enemy object.
    public static void KillEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        
    }

    //Starts the GameOverScreen when called.
    public void GameOver()
    {
        Debug.Log("GAME OVER!");
        gameOverUI.SetActive(true);
    }
}
