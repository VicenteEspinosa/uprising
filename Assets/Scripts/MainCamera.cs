using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class MainCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = .1f;

    private Vector3 velocity;

    public GameObject tutorialPrefab;

    private void Start()
    {
        int tutorial = PlayerPrefs.GetInt("tutorial");
        if (tutorial == 0)
        {
            Instantiate(tutorialPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void LateUpdate()
    {
        if (targets.All(player => player == null))
        {
            return;
        }

        Transform firstTarget = targets.First(player => player != null);
        var bounds = new Bounds(firstTarget.position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
            {
                continue;
            }
            bounds.Encapsulate(targets[i].position);
        }

        Move(bounds);
    }

    private void Move(Bounds bounds)
    {

        Vector3 centerPoint = bounds.center;

        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
}
