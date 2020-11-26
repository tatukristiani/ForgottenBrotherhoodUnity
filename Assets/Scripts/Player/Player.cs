using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public float health = 100f;
    }

    public Animator animator;
    public PlayerStats playerStats = new PlayerStats();

    public void DamagePlayer(float damage)
    {
        playerStats.health -= damage;
        if (playerStats.health <= 0)
        {
            animator.SetBool("PlayerIsDead", true);
            GameMaster.KillPlayer(this);
        }
    }
}

 


