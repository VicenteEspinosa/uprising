using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithItems : MonoBehaviour

{
    public AudioSource doorsound;

    [HideInInspector]
    public static bool isInteracting;
    public static bool isCollidingWithDoor;
    public static bool isCollidingWithFire;
    [SerializeField]
    float timeToUnlockDoor = 3;
    [SerializeField]
    float timeToExtinguishFire = 3;
    [SerializeField]
    GameObject CustomProgressBar;
    float startedInteractingTime;
    GameObject door;
    GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
        isInteracting = false;
        if (!GameObject.FindGameObjectWithTag("Canvas"))
        {
            // Create a Canvas
            GameObject canvas = new GameObject("Canvas");
            canvas.tag = "Canvas";
            canvas.AddComponent<Canvas>();
            canvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((isCollidingWithDoor || isCollidingWithFire) && !isInteracting)
        {
            // Start interacting
            if (Input.GetAxis("Interact Firefighter") != 0)
            {
                isInteracting = true;
                startedInteractingTime = Time.time;
                var progressBar = GameObject.Instantiate(CustomProgressBar);
                progressBar.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
                if (isCollidingWithDoor)
                {
                    doorsound.Play();
                    Debug.Log("Interacting with door");
                    Debug.Log(door.transform.position);
                    progressBar.transform.position = door.transform.position;
                }
                else if (isCollidingWithFire)
                {
                    progressBar.transform.position = fire.transform.position;
                }
            }
        }

        if (isInteracting)
        {
            if (isCollidingWithDoor)
            {
                if (Time.time > startedInteractingTime + timeToUnlockDoor)
                {
                    Destroy(door);
                    isCollidingWithDoor = false;
                    isInteracting = false;
                }
            }
            else if (isCollidingWithFire)
            {
                if (Time.time > startedInteractingTime + timeToExtinguishFire)
                {
                    Destroy(fire);
                    isCollidingWithFire = false;
                    isInteracting = false;
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            door = collision.gameObject;
            Debug.Log("ENTER Colliding with door");
            Debug.Log(door.transform.position);

            isCollidingWithDoor = true;
        }
        else if (collision.gameObject.tag == "Fire")
        {
            fire = collision.gameObject;
            isCollidingWithFire = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            isCollidingWithDoor = false;
        }
        else if (collision.gameObject.tag == "Fire")
        {
            isCollidingWithFire = false;
        }
    }
}
