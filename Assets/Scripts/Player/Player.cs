using UnityEngine;
using UnityEngine.SceneManagement;


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


        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("InfinityScene") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StoryScene"))
        {
            healthBar.SetMaxHealth(health);
        }

    }


    //This methdod deals the parameters amount of damage to player, subtracts it from player health and checks if player is dead.
    //If players health is <= 0, GameMasters method KillPlayer is called that destroys the player object.
    public void DamagePlayer(float damage)
    {
        health -= damage;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("InfinityScene") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StoryScene"))
        {
            healthBar.SetHealth(health);
        }

        if (health <= 0)
        {
            animator.SetBool("PlayerIsDead", true);
            GameMaster.KillPlayer(this);
        }
    }

    //start next level when colliding with the portal.
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Exit")
        {
            Invoke("Restart", 2f);

            enabled = false;
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(2);
    }

}
 


