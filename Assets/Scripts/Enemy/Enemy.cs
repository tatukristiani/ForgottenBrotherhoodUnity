using UnityEngine;
using UnityEngine.UI;


//Class has enemys health, controls healthbar slider and a method to damage the enemy.
//Also bosses -**-
public class Enemy : MonoBehaviour
{
    
    public Slider healthBarSlider;
    public GameObject enemyHealthBar;
    private float health;
    private float maxHealth = 80f;
    private float bossMaxHealth = 4000f;
    private float adjustHealth;

    private Animator animator;
    

    //We set the health to be maxhealth and deactivate the healthbar so its not yet visible.
    //sets the hp depengind on the object, enemy/boss
    private void Start()
    {
        if (gameObject.tag == "Enemy")
        {
            health = maxHealth;
            adjustHealth = maxHealth;
        }

        if (gameObject.tag == "Boss")
        {
            animator = GetComponent<Animator>();
            health = bossMaxHealth;
            adjustHealth = bossMaxHealth;
        }

        enemyHealthBar.SetActive(false);
    }


    //When enemy get damaged, activates healthbar, subtracts the parameters amount from health and adjusts the slider.
    //If enemys health goes to <= 0, GameMasters KillEnemy is called and it destroys the object.
    public void DamageEnemy(float damage)
    {
        enemyHealthBar.SetActive(true);
        health -= damage;
        healthBarSlider.value = AdjustSlider();
        if (health <= 0)
        {
            if(gameObject.tag == "Enemy")
            {
                GameMaster.KillEnemy(this);
            }

            if(gameObject.tag == "Boss")
            {
                enemyHealthBar.SetActive(false);
                animator.SetBool("bossIsDead", true);
                GameMaster.KillBoss(this);
            }
            
        }
    }

    public float GetHealth()
    {
        return health;
    }
    //Adjust the slider to current health.
    public float AdjustSlider()
    {
        return (health / adjustHealth);
    }
}
