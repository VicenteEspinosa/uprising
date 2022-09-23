using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceibeDamageFromMutant : MonoBehaviour
{
    [SerializeField]
    float health = 100f;
    [SerializeField]
    float mutantDamage = 30;
    // Start is called before the first frame update
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mutant")
        {
            TakeDamage(mutantDamage);
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
