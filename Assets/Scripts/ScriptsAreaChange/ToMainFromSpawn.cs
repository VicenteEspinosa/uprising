using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainFromSpawn : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        
        if (GameObject.FindGameObjectsWithTag("Tutorial").Length > 0)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Tutorial")[0]);
        }
        SceneManager.LoadScene("MainScene");
    }
}
