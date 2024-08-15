using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIcon_Rotation : MonoBehaviour
{
    public float rotationSpeed = 85f; // Adjust the speed of rotation

    void Update()
    {
        // Rotate smoothly in all directions
        transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed * Time.deltaTime);
    }
}
