using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the GameObject
        Rigidbody rb = GetComponent<Rigidbody>();

        // Check if the Rigidbody component exists
        if (rb != null)
        {
            // Freeze rotation along the X, Y and Z axes
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            //Freeze the position of the player to Z position
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;

        }
        else
        {
            Debug.LogWarning("Rigidbody component not found on the GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any update logic here if needed
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) // Check if the collider is the player
        {
            transform.position = new Vector3(0f, 2.39f, 0f);
            Debug.Log("Player has touched the obstacle collider");


            //Inform the GameManager about the game over state
            GameManager.Instance.GameOver();
        }
    }

}
