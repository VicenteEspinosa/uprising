using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoor : MonoBehaviour
{
    [HideInInspector]
    public static bool isInteracting;
    public static bool isCollidingWithDoor;
    [SerializeField]
    float timeToUnlock;
    [SerializeField]
    GameObject CustomProgressBar;
    float startedInteractingTime;
    GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        isInteracting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteracting)
        {
            if (Time.time > startedInteractingTime + timeToUnlock)
            {
                isInteracting = false;
                isCollidingWithDoor = false;
                Destroy(door);
            }
        }

        if (isCollidingWithDoor && !isInteracting)
        {
            // Start interacting
            if (Input.GetAxis("Interact Firefighter") != 0)
            {
                isInteracting = true;
                startedInteractingTime = Time.time;
                var progressBar = GameObject.Instantiate(CustomProgressBar);
                progressBar.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
                progressBar.transform.position = door.transform.position;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            door = collision.gameObject;
            isCollidingWithDoor = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            isCollidingWithDoor = false;
        }
    }
}
