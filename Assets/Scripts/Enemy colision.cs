using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemycolision : MonoBehaviour
{
    public float health;
    public float bulletDamage;
    public float axeDamage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet") TakeDamage(bulletDamage);
        if (!InteractDoor.isInteracting && Input.GetAxis("Atack Firefighter") != 0){
            if (collision.gameObject.tag == "Firefighter") TakeDamage(axeDamage);
        }
        

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
