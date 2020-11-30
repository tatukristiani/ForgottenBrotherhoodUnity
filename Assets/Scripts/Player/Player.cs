using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Player class has its own health. Controls the animation of dying when players health hits 0 or below. And a way to deal damage to player.
public class Player : MonoBehaviour
{

    private float health;
    private float maxHealth = 100;
    public HealthBar healthBar;
    private Animator animator;



    private void Start()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
        healthBar.SetMaxHealth(health);
    }
    public void DamagePlayer(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            animator.SetBool("PlayerIsDead", true);
            GameMaster.KillPlayer(this);
        }
    }
}

 


