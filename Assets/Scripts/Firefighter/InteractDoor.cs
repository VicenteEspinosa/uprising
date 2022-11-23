using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoor : MonoBehaviour

{
    public AudioSource doorsound;

    [HideInInspector]
    public static bool isInteracting;
    public static bool isCollidingWithDoor;
    public static bool isCollidingWithFire;
    [SerializeField]
    float timeToUnlock;
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
        if (isInteracting)
        {
            if (Time.time > startedInteractingTime + timeToUnlock)
            {
                isInteracting = false;
                if (isCollidingWithDoor)
                {
                    Destroy(door);
                    isCollidingWithDoor = false;
                }
                else if (isCollidingWithFire)
                {
                    Destroy(fire);
                    isCollidingWithFire = false;
                }
            }
        }

        if ((isCollidingWithDoor || isCollidingWithFire) && !isInteracting)
        {
            // Start interacting
            if (Input.GetAxis("Interact Firefighter") != 0)
            {
                isInteracting = true;
                startedInteractingTime = Time.time;
                doorsound.Play();
                var progressBar = GameObject.Instantiate(CustomProgressBar);
                progressBar.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
                if (isCollidingWithDoor)
                {
                    progressBar.transform.position = door.transform.position;
                }
                else if (isCollidingWithFire)
                {
                    progressBar.transform.position = fire.transform.position;
                }
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
        else if (collision.gameObject.tag == "Fire")
        {
            fire = collision.gameObject;
            isCollidingWithFire = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
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
