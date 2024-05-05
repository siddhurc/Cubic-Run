using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offsetPosition;

    public float movementSmoothness = 3f;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        offsetPosition = playerTransform.position + transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 finalPosition = playerTransform.position + offsetPosition;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, finalPosition, movementSmoothness);

        transform.position = smoothedPosition;

        //playerTransform.LookAt(playerTransform);
    }
}
