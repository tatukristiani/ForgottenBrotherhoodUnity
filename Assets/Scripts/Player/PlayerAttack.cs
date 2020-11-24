using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{


    public GameObject projectile;
    private GameObject spell;
    public Transform fireballPosition;
    public Animator animator;

    private float projectileForce = 10f;
   



    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("SpellCastTrigger");
            spell = Instantiate(projectile, fireballPosition.position, fireballPosition.rotation);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPosition = transform.position;
            Vector2 direction = (mousePosition - myPosition).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<Projectile>().damage = 20;
        }
    }
  
}
