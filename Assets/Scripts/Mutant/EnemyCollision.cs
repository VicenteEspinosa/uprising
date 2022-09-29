using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField]
    private float bulletDamage;

    [SerializeField]
    private float axeDamage;

    bool isCollidingWithFirefighter = false;

    GameObject Firefighter;
    GameObject Detective;


    // Start is called before the first frame update
    void Start()
    {
        Firefighter = GameObject.FindGameObjectsWithTag("Firefighter")[0];
        Detective = GameObject.FindGameObjectsWithTag("Detective")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!InteractDoor.isInteracting && Input.GetAxis("Atack Firefighter") != 0 && isCollidingWithFirefighter)
        {
            TakeDamage(axeDamage, gameObject);
        }
        if ((float)Variables.Object(gameObject).Get("Current Health") <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(bulletDamage, gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Firefighter")
        {
            isCollidingWithFirefighter = true;
            TakeDamage((float)Variables.Object(gameObject).Get("Damage"), Firefighter);
        }
        else if (collision.gameObject.tag == "Detective")
        {
            TakeDamage((float)Variables.Object(gameObject).Get("Damage"), Detective);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Firefighter")
        {
            isCollidingWithFirefighter = false;
        }
    }

    public void TakeDamage(float damage, GameObject damageReceiver)
    {
        Variables.Object(damageReceiver).Set("Current Health", (float)Variables.Object(damageReceiver).Get("Current Health")
            - damage);
    }
}
