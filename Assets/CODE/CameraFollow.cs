using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target object for the camera to follow (usually your player)
    public float smoothing = 5f; // The speed with which the camera will be following.

    private Vector3 _offset; // The initial offset from the target.
    private Vector3 _targetCamPos;

    void Start()
    {
        // Calculate the initial offset by subtracting the Camera's position from the target's position.
        _offset = transform.position - target.position;
    }

    void FixedUpdate() // FixedUpdate is called once per physics update
    {
        if (target == null) return;

        // Create a position the camera is aiming for based on the offset from the target.
        _targetCamPos = target.position + _offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, _targetCamPos, smoothing * Time.deltaTime);
    }
}
