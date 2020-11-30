using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Player class has its own health. Controls the animation of dying when players health hits 0 or below. And a way to deal damage to player.
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

 


