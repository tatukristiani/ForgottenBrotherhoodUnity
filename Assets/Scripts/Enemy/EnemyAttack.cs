using UnityEngine;
using UnityEngine.SceneManagement;


//EnemyAttack class handles the enemy "AI" and how it reacts to player position.
//Lots of player != nulls can be found due to couple of errors in the past :D. Too scared to take them off.
public class EnemyAttack : MonoBehaviour
{
   
    private float chaseSpeed; //speed of enemy
    private float idleDistance = 6f; //the distance when enemy is idle.
    private float retreatDistance = 4f; //the distance when enemy starts to walk away from player while still shooting
    private float detectDistance; //the distance when the enemy is able to detect player.
    private float timeBtwShots; //enemys shooting cooldown
    private float startTimeBtwShots = 2f;
    private bool attemptedChase = false;
    private bool enemyFacingRight = true;

    public GameObject projectile;
    private Transform player;
    private GameObject playerPrefab;
    public Transform spellPosition;
    private Animator animator;
    private Vector3 originalPosition;


    //On the Start() we check if we are on the infinity scene, set the detect distance so the enemies detect allways the player and increase the chase speed. 
    void Start()
    {

        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        
        if (playerPrefab != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.Log(player);
            animator = GetComponent<Animator>();
            timeBtwShots = startTimeBtwShots;
            originalPosition = gameObject.transform.position;

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

        if (player != null)
        {
          
            //If player is in distance it starts to chase/shoot. This allways happends in infinity mode.
            if (player != null && DistanceBetweenPlayerAndEnemy() < detectDistance)
            {
                Chase();
            }

            //If player is not in detect distance the enemy goes back to its original position.
            else if (player != null && DistanceBetweenPlayerAndEnemy() > detectDistance)
            {
                Flee();

            }


            //Shooting cooldown.
            if (timeBtwShots <= 0)
            {

                animator.SetTrigger("EnemyUseSpell");

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


    //returns to original position where the enemy started chasing.
    private void Flee()
    {
        ReturnToOriginalDirection();

        if (attemptedChase)
        {
            animator.SetFloat("EnemySpeed", chaseSpeed);
            transform.position = Vector2.MoveTowards(transform.position, originalPosition, chaseSpeed * Time.deltaTime);

            if (transform.position == originalPosition)
            {
                attemptedChase = false;
            }

        }
        else
        {
            animator.SetFloat("EnemySpeed", 0f);
            transform.position = this.transform.position;
        }
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
                animator.SetFloat("EnemySpeed", chaseSpeed);
            }

            //Stands still and keeps shooting. Animator set to stand still.
            else if (DistanceBetweenPlayerAndEnemy() < idleDistance && DistanceBetweenPlayerAndEnemy() > retreatDistance)
            {
                transform.position = this.transform.position;
                animator.SetFloat("EnemySpeed", 0f);
            }

            //Enemy starts to walk backward away from player and keeps shooting.
            else if (DistanceBetweenPlayerAndEnemy() < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -chaseSpeed * Time.deltaTime);
                animator.SetFloat("EnemySpeed", chaseSpeed);
            }
            attemptedChase = true;
        }
    }

    //Flips the enemy by checking where the player is and the enemy is currently.
    private void ChaseDirection()
    {

        if(player.position.x < transform.position.x)
        {
            if(enemyFacingRight)
            {
                Flip();
            }  
        }
        else if(player.position.x > transform.position.x)
        {
            if(!enemyFacingRight)
            {
                Flip();
            }   
        }
    }


    //Flips the enemy depending on the direction where it is going when returning to original position by using the difference between original position and current position.
    private void ReturnToOriginalDirection()
    {
        if(transform.position.x > originalPosition.x)
        {
            if(enemyFacingRight)
            {
                Flip();
            }  
        }
        else if(transform.position.x < originalPosition.x)
        {
            if(!enemyFacingRight)
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

