using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody playerRb;
    private Animator playerAnimator;
    private ObstacleManager obstacleManager;

    public float movement_speed = 0.10f;
    public float jump_speed = 5f;

    public bool isActionInProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        // Register a callback for when the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Get the Rigidbody component attached to the GameObject
        playerRb = GetComponent<Rigidbody>();

        // Check if the Rigidbody component exists
        if (playerRb != null)
        {
            // Freeze rotation along the X, Y and Z axes
            playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            Debug.LogWarning("Rigidbody component not found on the GameObject.");
        }

        gameManager = GameManager.FindObjectOfType<GameManager>();
        obstacleManager = ObstacleManager.FindObjectOfType<ObstacleManager>();

        playerAnimator = GetComponent<Animator>();
    }

    void OnDestroy()
    {
        // Unregister the callback when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reinitialize references when a new scene is loaded
        gameManager = GameManager.FindObjectOfType<GameManager>();
        obstacleManager = ObstacleManager.FindObjectOfType<ObstacleManager>();
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

            //Inform the GameManager about the game over state
            gameManager.GameOver();
            
        }
        else if (other.CompareTag("Obstacle_burst"))
        {
            gameManager.PlayerTakeDamage(25);
        }

        //Collected gold bar with a value of 10
        if (other.CompareTag("Collectables_01")) // Check if the collider is the player
        {
            gameManager.IncreaseScore(10);

            //disble the goldbar since it is collected
            other.gameObject.SetActive(false);
        }
    }

    public void LeftMove(Vector2 swipeDirection)
    {
        // Ensure playerRb is not null and there's a Rigidbody component attached
        if (playerRb != null)
        {
            // Calculate the force to apply for left movement
            Vector3 force = -transform.right * movement_speed * swipeDirection.magnitude;

            // Apply the force to the Rigidbody
            playerRb.AddForce(force, ForceMode.Impulse);
        }
    }

    public void RightMove(Vector2 swipeDirection)
    {
        // Ensure playerRb is not null and there's a Rigidbody component attached
        if (playerRb != null)
        {
            // Calculate the force to apply for right movement
            Vector3 force = transform.right * movement_speed * swipeDirection.magnitude;

            // Apply the force to the Rigidbody
            playerRb.AddForce(force, ForceMode.Impulse);
        }
    }

    public void JumpMove(Vector2 swipeDirection)
    {
        // Ensure playerRb is not null and there's a Rigidbody component attached
        if (playerRb != null && !isActionInProgress)
        {
            isActionInProgress = true;

            // Calculate the force to apply for left movement
            Vector3 force = transform.up * jump_speed;

            // Apply the force to the Rigidbody
            playerRb.AddForce(force, ForceMode.Impulse);

            // Activate the Jump mechanism
            playerAnimator.SetTrigger("Jump");
        }
    }

    public void CrouchMove(Vector2 swipeDirection)
    {
        // Ensure playerRb is not null and there's a Rigidbody component attached
        if (playerRb != null && !isActionInProgress)
        {
            isActionInProgress = true;

            // Activate the crouch mechanism
            playerAnimator.SetTrigger("Crouch");
        }
    }

    public void WeaponFired()
    {
        playerAnimator.SetTrigger("FireWeapon");
        //playerAnimator.ResetTrigger("FireWeapon");
    }


    //being called after the action event.
    // It is being called at the end of animation event
    public void resetActionInProgress(int temp)
    {
        isActionInProgress = false;
        //Debug.Log("Action has been reset.!!");
        playerAnimator.ResetTrigger("FireWeapon");
    }
}
