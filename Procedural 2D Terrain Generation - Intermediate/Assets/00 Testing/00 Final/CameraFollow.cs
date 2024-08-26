using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;       // The player's transform
    [SerializeField] private Vector3 offset = new Vector3(5f, 0f, -10f); // The offset from the player
    [SerializeField] private float smoothing = 0.125f; // The smoothing factor for camera movement

    private Vector3 velocity = Vector3.zero; // Current velocity, used by SmoothDamp

    private void LateUpdate()
    {
        // Calculate the target position for the camera
        Vector3 targetPosition = player.position + offset;

        // Smoothly interpolate the camera's position between its current position and the target position
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;
    }
}
