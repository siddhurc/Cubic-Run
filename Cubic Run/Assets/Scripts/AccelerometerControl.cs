using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AccelerometerControl : MonoBehaviour
{
    Accelerometer accelerometer;
    [SerializeField] private float tiltSensitivity = 14f;


    // Start is called before the first frame update
    void Start()
    {
        accelerometer = Accelerometer.current;
    }

    // Update is called once per frame
    void Update()
    {
#if !UNITY_EDITOR
        float tilt_x = accelerometer.acceleration.ReadValue().x;

        tilt_x = Mathf.Clamp(tilt_x, -1.0f, 1.0f);

        Vector3 playerMovement = new Vector3(tilt_x * tiltSensitivity, 0, 0);

        gameObject.transform.Translate(playerMovement * Time.deltaTime);
#endif
    }

    private void OnEnable()
    {

#if !UNITY_EDITOR
        //enable accelerometrer
        InputSystem.EnableDevice(Accelerometer.current);
#endif
    }

    private void OnDisable()
    {

#if !UNITY_EDITOR
        //disable accelerometer
        InputSystem.DisableDevice(Accelerometer.current);
#endif
    }
}
