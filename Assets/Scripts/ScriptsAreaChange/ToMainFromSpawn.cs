using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainFromSpawn : MonoBehaviour
{
    public GameObject cluePrefab;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("Clue").Length);
        if (collision.gameObject.CompareTag("Detective") || collision.gameObject.CompareTag("Firefighter"))
        {
            if (GameObject.FindGameObjectsWithTag("Clue").Length > 0)
            {
                Instantiate(cluePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                if (GameObject.FindGameObjectsWithTag("Tutorial").Length > 0)
                {
                    Destroy(GameObject.FindGameObjectsWithTag("Tutorial")[0]);
                }
                // SceneManager.LoadScene("MainScene");
            }
        }
    }
}
