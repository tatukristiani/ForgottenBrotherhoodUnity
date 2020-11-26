using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
   
    private float chaseSpeed;
    private float idleDistance = 6f;
    private float retreatDistance = 4f;
    private float detectDistance;
    private float timeBtwShots;
    private float startTimeBtwShots = 2f;
    private bool attemptedChase = false;
    private bool enemyFacingRight = true;

    public GameObject projectile;
    private Transform player;
    public Transform spellPosition;
    private Animator animator;
    private Vector3 originalPosition;


    



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
           
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


            //BUGG HERE
            if (player != null && DistanceBetweenPlayerAndEnemy() < detectDistance)
            {
                Chase();
            }
            else if (player != null && DistanceBetweenPlayerAndEnemy() > detectDistance)
            {
                Flee();

            }

            if (timeBtwShots <= 0)
            {
                //BUG HERE
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

    private float DistanceBetweenPlayerAndEnemy()
    {
        return Vector2.Distance(transform.position, player.position);
    }

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

    private void Chase()
    {
        if (player != null)
        {
            ChaseDirection();

            if (DistanceBetweenPlayerAndEnemy() > idleDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
                animator.SetFloat("EnemySpeed", chaseSpeed);
            }

            else if (DistanceBetweenPlayerAndEnemy() < idleDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
                animator.SetFloat("EnemySpeed", 0f);
            }
            else if (DistanceBetweenPlayerAndEnemy() < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -chaseSpeed * Time.deltaTime);
                animator.SetFloat("EnemySpeed", chaseSpeed);
            }
            attemptedChase = true;
        }

    }

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

