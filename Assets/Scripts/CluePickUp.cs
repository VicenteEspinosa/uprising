using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePickUp : MonoBehaviour
{
    public GameObject textPrefab;

    private bool pickUpAllowed;
    private GameObject text;

    // Update is called once per frame
    void Update()
    {
        if (pickUpAllowed && Input.GetMouseButtonDown(1))
        {
            PickUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hola");
        if (collision.gameObject.name.Equals("Detective"))
        {
            Debug.Log("hola2");
            Transform clue = gameObject.GetComponent<Transform>();
            text = Instantiate(textPrefab, clue.position, clue.rotation);
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Detective"))
        {
            Destroy(text);
            pickUpAllowed = false;
        }
    }

    private void PickUp()
    {
        Destroy(gameObject);
    }
}
