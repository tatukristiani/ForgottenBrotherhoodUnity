using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject fireballImpactEffect;
  
    public float damage = 20f;
    


    //on enemy hit deals damage to enemy and explodes, destory projectile object.
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag != "Player" && collision.tag != "Spell")
        {
            Instantiate(fireballImpactEffect, transform.position, Quaternion.identity);
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.DamageEnemy(damage);
            }
            Destroy(gameObject);
        }
           
    }
}
