using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CluePickUp : MonoBehaviour
{
    public GameObject textPrefab;
    public GameObject scoreTextPrefab;

    private GameObject internalScoreText;
    private int scoreNumber;
    private bool pickUpAllowed;
    private GameObject text;
    private GameObject clueObject;

    void Start()
    {
        Transform detective = gameObject.GetComponent<Transform>();
        internalScoreText = Instantiate(scoreTextPrefab, detective.position, detective.rotation);
        scoreNumber = 0;
        internalScoreText.GetComponentInChildren<TextMeshProUGUI>().text = "Score: " + scoreNumber;
    }

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
        if (collision.gameObject.name.Equals("Pista1"))
        {
            Transform clue = collision.GetComponent<Transform>();
            text = Instantiate(textPrefab, clue.position, clue.rotation);
            clueObject = collision.gameObject;
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Pista1"))
        {
            Destroy(text);
            pickUpAllowed = false;
        }
    }

    private void PickUp()
    {
        Destroy(clueObject);
        scoreNumber++;
        internalScoreText.GetComponentInChildren<TextMeshProUGUI>().text = "Score: " + scoreNumber;
    }
}
