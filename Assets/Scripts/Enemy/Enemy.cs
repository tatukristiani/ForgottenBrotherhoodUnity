using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Class has enemys health, controls healthbar slider and a method to damage the enemy.
public class Enemy : MonoBehaviour
{
    

    [System.Serializable]
    public class EnemyStats
    {
        public float health = 100f;
    }

    public EnemyStats enemyStats = new EnemyStats();

    public Slider healthBarSlider;
    public GameObject enemyHealthBar;
    public float maxHealth = 100f;

    private void Start()
    {  
        enemyHealthBar.SetActive(false);
    }


    public void DamageEnemy(float damage)
    {
        enemyHealthBar.SetActive(true);
        enemyStats.health -= damage;
        healthBarSlider.value = AdjustSlider();
        if (enemyStats.health <= 0)
        {
            GameMaster.KillEnemy(this);
        }
    }


    private float AdjustSlider()
    {
        return (enemyStats.health / maxHealth);
    }
}
