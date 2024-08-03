using System.Collections;
using UnityEngine;

public class CameraBoundary_Manager : MonoBehaviour
{
    public Transform player;
    private GameManager gameManager;

    bool is_Offset_z_value_negative = false;
    bool canCheckPlayerOutOfCameraRange = false; // New flag

    public Vector3 desiredCameraOffset = new Vector3(0.35f, 5.50f, -5.76f);

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameManager.FindAnyObjectByType<GameManager>();
        StartCoroutine(StartCheckingAfterDelay(3.0f)); // Start the coroutine with a 3-second delay
    }

    void Update()
    {
        if (canCheckPlayerOutOfCameraRange)
        {
            checkPlayerOutofCameraRange();
        }
        ReorientCamera();
    }

    void ReorientCamera()
    {
        // Calculate the new camera position
        Vector3 newCameraPosition = player.position + desiredCameraOffset;
        transform.position = Vector3.Lerp(transform.position, newCameraPosition, Time.deltaTime);
    }

    void checkPlayerOutofCameraRange()
    {
        if ((transform.position - player.transform.position).z >= -1.00f)
        {
            gameManager.GameOver();
        }
    }

    IEnumerator StartCheckingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canCheckPlayerOutOfCameraRange = true; // Enable the flag after the delay
    }
}
