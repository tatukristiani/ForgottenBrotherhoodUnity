using UnityEngine;


//This class is allmost a copy of enemys projectile with slight changes.
//Due to lack of time, didn't realise that these changes should've been made in enemyprojectile. Much simpler.
public class BossProjectile : MonoBehaviour
{
    public GameObject fireballImpactEffect;

    private Rigidbody2D rb;
    private GameObject player;
    private Vector2 fireDirection;

    private float damage = 20f;
    private float speed = 15f;

    private bool projectileFacingRight = true;



    //We find player object and give direction and speed for the projectile.
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {

            rb = GetComponent<Rigidbody2D>();
            fireDirection = (player.transform.position - transform.position).normalized * speed; //projectiles direction is going towards the player current positon when the projectile is first created.
            rb.velocity = new Vector2(fireDirection.x, fireDirection.y);

            if (player.transform.position.x < transform.position.x)
            {
                if (projectileFacingRight)
                {
                    Flip();
                }
            }
            else if (player.transform.position.x > transform.position.x)
            {
                if (!projectileFacingRight)
                {
                    Flip();
                }
            }
        }
    }

    //on player hit, explodes and deals damage to player. Destroys projectile object. Explodes on wall, but no damage applied.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Boss" && collision.tag != "Spell")
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

    private void Flip()
    {
        // Switch the way the player is labelled as facing
        projectileFacingRight = !projectileFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

