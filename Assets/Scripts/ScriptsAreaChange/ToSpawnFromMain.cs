using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSpawnFromMain : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        SceneManager.LoadScene("Spawn");
    }
}
