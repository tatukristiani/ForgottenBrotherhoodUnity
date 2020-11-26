using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    
    public GameObject projectile;
    public Transform fireballPosition;
    public Animator animator;
    

    private float projectileForce = 15f;



    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        
        animator.SetTrigger("SpellCastTrigger");
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 myPosition = fireballPosition.position;
        Vector2 direction = (mousePosition - myPosition).normalized;
        GameObject spell = Instantiate(projectile, fireballPosition.position, fireballPosition.rotation);
        spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
        spell.GetComponent<Projectile>().damage = 20;
    }
  
}
