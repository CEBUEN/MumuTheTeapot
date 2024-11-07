using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public float xOffset = 2f;
    public Transform target;

    private float fixedZoomLevel = 32f; // This sets the zoom for the 0.25x scale

    // Start is called before the first frame update
    void Start()
    {
        SetCameraZoom();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x + xOffset, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);

        // Ensure the zoom level is locked
        SetCameraZoom();
    }

    private void SetCameraZoom()
    {
        Camera.main.orthographicSize = fixedZoomLevel;
    }
}
