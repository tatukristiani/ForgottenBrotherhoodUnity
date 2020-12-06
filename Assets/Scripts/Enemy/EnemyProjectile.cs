using UnityEngine;


//This class handles the movement of enemys projectile AKA Fireblast and what happens when it collides.
public class EnemyProjectile : MonoBehaviour
{
    public GameObject fireballImpactEffect;

    private Rigidbody2D rb;
    private GameObject player;
    private Vector2 fireDirection;

    private bool enemyProjectileFacingRight = true;
    private float damage = 10f;
    private float speed = 8f;


    //We find player object and give direction and speed for the projectile.
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        if (player != null)
        { 
           
            rb = GetComponent<Rigidbody2D>();
            fireDirection = (player.transform.position - transform.position).normalized * speed; //projectiles direction is going towards the player current positon when the projectile is first created.
            rb.velocity = new Vector2(fireDirection.x, fireDirection.y);


            //we flip the projectile to right/left by figuring the x position of players and projectiles position.
            if (player.transform.position.x < transform.position.x)
            {
                if (enemyProjectileFacingRight)
                {
                    Flip();
                }
            }
            else if (player.transform.position.x > transform.position.x)
            {
                if (!enemyProjectileFacingRight)
                {
                    Flip();
                }
            }
        } 
    }


    //on player hit, explodes and deals damage to player. Destroys projectile object. Explodes on wall, but no damage applied.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //wont collide with enemy or other spells.
        if (collision.tag != "Enemy" && collision.tag != "Spell")
        {
            Instantiate(fireballImpactEffect, transform.position, Quaternion.identity);
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.DamagePlayer(damage);
            }
            Destroy(gameObject);
        }
    }


    //Flips the character/projectile from right -> left and left -> right
    private void Flip()
    {
        // Switch the way the player is labelled as facing
        enemyProjectileFacingRight = !enemyProjectileFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
