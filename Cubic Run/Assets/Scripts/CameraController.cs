using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player GameObject
    public float followDistance = 3f; // Distance behind the player to maintain

    private Vector3 initialOffset; // Initial offset between the player and the camera
    public float movementSmoothness = 3f;

    void Start()
    {
        // Calculate the initial offset between the player and the camera
        initialOffset = transform.position - player.position;
        //Debug.Log(initialOffset);
    }

    void LateUpdate()
    {
        // Calculate the target position for the camera
        Vector3 targetPosition = initialOffset + player.position;

        targetPosition.z -= followDistance;
        // Update the camera position smoothly using Lerp
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSmoothness);
    }
}
