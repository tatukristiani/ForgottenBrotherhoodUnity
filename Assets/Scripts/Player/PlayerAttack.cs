using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class handles player shooting.
public class PlayerAttack : MonoBehaviour
{

    
    public GameObject projectile;
    public Transform fireballPosition;
    public Animator animator;
    private Vector2 mousePosition;
    private Vector2 direction;
    private Vector2 myPosition;

   
    private float projectileForce = 15f;


    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //where mouse is
        myPosition = fireballPosition.position; //shooting startingpoint
        direction = (mousePosition - myPosition); //direction is towards mouse
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //rotates projectile

        if (Input.GetMouseButtonDown(0))
        {
            float distance = direction.magnitude;
            Vector2 dir = direction / distance;
            dir.Normalize();
            Shoot(dir, rotationZ);
        }
    }

  
    //method need direction where to shoot and rotation of projectile
    private void Shoot(Vector2 direction, float rotationZ)
    {  
        animator.SetTrigger("SpellCastTrigger");
        GameObject spell = Instantiate(projectile) as GameObject; 
        spell.transform.position = fireballPosition.transform.position;
        spell.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
        spell.GetComponent<Projectile>().damage = 20;
    }
}
