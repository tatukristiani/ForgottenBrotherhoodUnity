using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;
    public GameObject player;
    private Animator playerAnimator;

    public float health;


    void Awake()
    {
        if (playerStats != null)
        {
            Destroy(playerStats);
        }
        else
        {
            playerStats = this;
        }
        DontDestroyOnLoad(this);
    }


    void Start()
    {
        health = 100;
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();

    }

    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
    }

    public void HealPlayerToFull()
    {
        health = 100;
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {

            playerAnimator.SetBool("PlayerIsDead", true);
            Destroy(player, 1.5f);
        }
    }
}

 


