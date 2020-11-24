using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject fireballImpactEffect;
    private Transform player;
    private Vector2 target;
    
    private float damage;
    public float speed;
   

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        damage = 40f;
        
    }


    void Update()
    {
        if (player != null)
        {


            //Enemy shoot spell on players current location, when reached it explodes.
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (transform.position.x == target.x && transform.position.y == target.y)
            {
                Destroy(gameObject);

            }
        }

        if(player == null)
        {
            Destroy(gameObject);
        }
    }

    //on player hit, explodes and deals damage to player. Destroys projectile object.
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Player" && collision.tag != "Spell")
        {
            Instantiate(fireballImpactEffect, transform.position, Quaternion.identity);
            PlayerStats.playerStats.DealDamage(damage);
           
            Destroy(gameObject);
        }
    }
}
