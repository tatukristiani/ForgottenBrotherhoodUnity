using UnityEngine;
using UnityEngine.SceneManagement;


//EnemyAttack class handles the enemy "AI" and how it reacts to player position.
//Lots of player != nulls can be found due to couple of errors in the past :D. Too scared to take them off.
public class Boss : MonoBehaviour
{

    private float chaseSpeed; //speed of enemy
    private float idleDistance = 8f; //the distance when enemy is idle.
    private float retreatDistance = 3f; //the distance when enemy starts to walk away from player while still shooting
    private float detectDistance; //the distance when the enemy is able to detect player.
    private float timeBtwShots; //enemys shooting cooldown
    private float startTimeBtwShots = 2f;
    private bool enemyFacingRight = true;

    public GameObject projectile;
    private Transform player;
    public Transform spellPosition;
    private Animator animator;
    

    //On the Start() we check if we are on the infinity scene, set the detect distance so the enemies detect allways the player and increase the chase speed. 
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {

            animator = GetComponent<Animator>();
            timeBtwShots = startTimeBtwShots;

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("InfinityScene"))
            {
                detectDistance = 100f;
                chaseSpeed = 2f;
            }
            else
            {
                detectDistance = 15f;
                chaseSpeed = 1f;
            }
        }

    }


    void Update()
    {

        if (player != null && !animator.GetBool("bossIsDead"))
        {

            //If player is in distance it starts to chase/shoot. This allways happends in infinity mode.
            if (player != null && DistanceBetweenPlayerAndEnemy() < detectDistance)
            {
                Chase();
            }


            //Shooting cooldown.
            if (timeBtwShots <= 0)
            {

                //animator.SetTrigger("EnemyUseSpell");

                Instantiate(projectile, spellPosition.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }


    //Gets the distance between player and enemy.
    private float DistanceBetweenPlayerAndEnemy()
    {
        return Vector2.Distance(transform.position, player.position);
    }


    //Chases player and changes movement according to distance between player and enemy.
    private void Chase()
    {
        if (player != null)
        {
            ChaseDirection();

            //Chase player, animator set so the enemy is moving
            if (DistanceBetweenPlayerAndEnemy() > idleDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
                animator.SetFloat("bossSpeed", chaseSpeed);
            }

            //Stands still and keeps shooting. Animator set to stand still.
            else if (DistanceBetweenPlayerAndEnemy() < idleDistance && DistanceBetweenPlayerAndEnemy() > retreatDistance)
            {
                transform.position = this.transform.position;
                animator.SetFloat("bossSpeed", 0f);
            }

            //Enemy starts to walk backward away from player and keeps shooting.
            else if (DistanceBetweenPlayerAndEnemy() < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -chaseSpeed * Time.deltaTime);
                animator.SetFloat("bossSpeed", chaseSpeed);
            }
        }
    }

    //Flips the enemy by checking where the player is and the enemy is currently.
    private void ChaseDirection()
    {

        if (player.position.x < transform.position.x)
        {
            if (enemyFacingRight)
            {
                Flip();
            }
        }
        else if (player.position.x > transform.position.x)
        {
            if (!enemyFacingRight)
            {
                Flip();
            }
        }
    }


    //Flips the way that character is facing.
    private void Flip()
    {
        // Switch the way the player is labelled as facing
        enemyFacingRight = !enemyFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

