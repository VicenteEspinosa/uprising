using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MainCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = .1f;
    public float minZoom = 130f;
    public float maxZoom = 60f;
    public float zoomLimiter = 20f;

    private Vector3 velocity;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        Move(bounds);
        Zoom(bounds);
    }

    private void Zoom(Bounds bounds)
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, Mathf.Max(bounds.size.x, bounds.size.y) / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    private void Move(Bounds bounds)
    {

        Vector3 centerPoint = bounds.center;

        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
}
