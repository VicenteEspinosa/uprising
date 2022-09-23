using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNav : MonoBehaviour
{
    public GameObject player;
    public GameObject player2;
    private GameObject moveto;
    public float speed;
    public float sightRange;

    private float distance = 100000;
    private float distance2 = 100000;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Firefighter");
        player2 = GameObject.Find("Detective");
        
    }

    // Update is called once per frame
    void Update()
    {   
        CheckDistances();

        if(distance<distance2)
        {          
            MoveTowards(player);
        } else if (distance2 < distance){
            MoveTowards(player2);
        }
        
    }

    void CheckDistances()
    {
        if (player != null){
            distance = Vector2.Distance(transform.position, player.transform.position);
        } else {
            distance = 100000;
        }
            
        if (player2 != null){
            distance2 = Vector2.Distance(transform.position, player2.transform.position);
        } else {
            distance2 = 100000;
        }
    }

    void MoveTowards(GameObject jugador)
    {
        Vector2 direction = jugador.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x)* Mathf.Rad2Deg;
            
            if(distance < sightRange)
            {   transform.position = Vector2.MoveTowards(this.transform.position, jugador.transform.position, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
    }
}
