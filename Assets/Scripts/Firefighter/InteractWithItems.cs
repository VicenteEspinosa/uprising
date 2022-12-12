using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithItems : MonoBehaviour

{
    public AudioSource doorsound;
    [SerializeField]
    private GameObject FireSound;

    [HideInInspector]
    public static bool isInteracting;
    public static bool isCollidingWithDoor;
    public static bool isCollidingWithFire;
    [SerializeField]
    float timeToUnlockDoor = 3;
    [SerializeField]
    float timeToExtinguishFire = 3;
    [SerializeField]
    float timeBetweenKeys = 1;
    float timeOfLastKey;
    bool waitingForKeyInput = false;
    [SerializeField]
    GameObject[] keys;
    [SerializeField]
    string[] keysMeaning;
    GameObject currentKey;
    string expectedInput;

    [HideInInspector]
    public GameObject progressBar;
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
                timeOfLastKey = Time.time;
                isInteracting = true;
                startedInteractingTime = Time.time;
                progressBar = GameObject.Instantiate(CustomProgressBar);
                progressBar.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
                if (isCollidingWithDoor)
                {
                    doorsound.Play();
                    progressBar.transform.position = door.transform.position;
                    progressBar.GetComponent<ProgressBar>().StartProgress(timeToUnlockDoor);
                }
                else if (isCollidingWithFire)
                {
                    GameObject fireSound = Instantiate<GameObject>(FireSound);
                    // set time of fireSound to time left to extinguish fire
                    fireSound.GetComponent<AudioSource>().time = fireSound.GetComponent<AudioSource>().clip.length - timeToExtinguishFire;
                    progressBar.transform.position = fire.transform.position;
                    progressBar.GetComponent<ProgressBar>().StartProgress(timeToExtinguishFire);
                }
            }
        }

        if (isInteracting)
        {
            if (progressBar == null)
            {
                isInteracting = false;
                if (isCollidingWithDoor)
                {
                    Destroy(door);
                }
                else if (isCollidingWithFire)
                {
                    Destroy(fire);
                }
                isCollidingWithDoor = false;
                isCollidingWithFire = false;
            }

            else if (!waitingForKeyInput && Time.time > timeOfLastKey + timeBetweenKeys)
            {
                CreateRandomKey();
                waitingForKeyInput = true;
                progressBar.GetComponent<ProgressBar>().PauseProgress();
            }
            else if (waitingForKeyInput)
            {
                if (Input.GetKey(expectedInput))
                {
                    Destroy(currentKey);
                    timeOfLastKey = Time.time;
                    waitingForKeyInput = false;
                    progressBar.GetComponent<ProgressBar>().ResumeProgress();
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
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

    public void CreateRandomKey()
    {
        int randomIndex = Random.Range (0, keys.Length);
        currentKey = Instantiate<GameObject>(keys[randomIndex]);
        expectedInput = keysMeaning[randomIndex];
        currentKey.transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z - 1);
    }
}
