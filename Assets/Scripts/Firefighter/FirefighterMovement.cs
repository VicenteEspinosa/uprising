using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirefighterMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;
    Quaternion toRotation;
    float verticalInput;
    float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (InteractDoor.isInteracting)
        {
            horizontalInput = 0f;
            verticalInput = 0f;
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal Firefighter");
            verticalInput = Input.GetAxis("Vertical Firefighter");
        }

        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();
        
        transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);

        if (movementDirection != Vector2.zero)
        {
            toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }
}
