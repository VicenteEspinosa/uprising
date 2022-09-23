using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideFirefighter : MonoBehaviour
{
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
                Destroy(gameObject);
            }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Firefighter")
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
}
