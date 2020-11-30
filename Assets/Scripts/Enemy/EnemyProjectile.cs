using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject fireballImpactEffect;

    private Rigidbody2D rb;
    private GameObject player;
    private Vector2 fireDirection;

    private float damage = 10f;
    private float speed = 8f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        { 
           
            rb = GetComponent<Rigidbody2D>();
            fireDirection = (player.transform.position - transform.position).normalized * speed;

        } 
    }

    private void Update()
    {
        if (player != null)
        {
            rb.velocity = new Vector2(fireDirection.x, fireDirection.y);
        }
    }


    //on player hit, explodes and deals damage to player. Destroys projectile object. Explodes on wall, but no damage applied.
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
