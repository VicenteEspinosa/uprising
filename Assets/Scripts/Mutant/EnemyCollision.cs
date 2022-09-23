using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColision : MonoBehaviour
{
    public float health;
    public float bulletDamage;
    public float axeDamage;

    bool isCollidingWithFirefighter = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!InteractDoor.isInteracting && Input.GetAxis("Atack Firefighter") != 0 && isCollidingWithFirefighter)
        {
            TakeDamage(axeDamage);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(bulletDamage);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Firefighter")
        {
            isCollidingWithFirefighter = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Firefighter")
        {
            isCollidingWithFirefighter = false;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
