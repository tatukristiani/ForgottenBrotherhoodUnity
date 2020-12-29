using UnityEngine;


//This class handles the movement of enemys projectile AKA Fireblast and what happens when it collides.
public class EnemyProjectile : MonoBehaviour
{
    public GameObject fireballImpactEffect;

    private Rigidbody2D rb;
    private GameObject player;
    private Vector2 fireDirection;

    private float damage = 10f;
    private float speed = 8f;


    //We find player object and give direction and speed for the projectile.
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        if (player != null)
        { 
           
            rb = GetComponent<Rigidbody2D>();
            fireDirection = (player.transform.position - transform.position); 
            float rotationZ = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;

            float distance = fireDirection.magnitude;
            Vector2 dir = fireDirection / distance;
            dir.Normalize();
            EnemyShoot(dir, rotationZ);

        } 
    }


    private void EnemyShoot(Vector2 direction, float rotation)
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation);
        rb.velocity = direction * speed;
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
}
