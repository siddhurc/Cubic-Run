using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBarRotation : MonoBehaviour
{

    public float rotationSpeed = 85f; // Adjust the speed of rotation

    void Update()
    {
        // Rotate smoothly in all directions
        transform.Rotate(new Vector3(1, 1, 1) * rotationSpeed * Time.deltaTime);
    }
}
