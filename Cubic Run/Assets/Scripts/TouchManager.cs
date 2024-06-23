using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    public float movement_speed = 0.25f;

    // Touch controls
    private TouchInputActions touchInputActions;
    [SerializeField] private float minimumSwipeMagnitude = 2f;
    private Vector2 swipeStartPosition;
    private bool isSwipeInProgress = false;

    public GameObject player;
    private Rigidbody playerRb;
    private Animator playerAnimator;

    private void Awake()
    {
        touchInputActions = new TouchInputActions();
    }

    private void OnEnable()
    {
        touchInputActions.Enable();
    }

    private void OnDisable()
    {
        touchInputActions.Disable();
    }

    private void Start()
    {
        // Subscribe to swipe events
        touchInputActions.TouchInputMovement.Swipe.started += ctx => swipeStartPosition = ctx.ReadValue<Vector2>();
        touchInputActions.TouchInputMovement.Swipe.canceled += ctx => ProcessSwipe(ctx.ReadValue<Vector2>());

        //Getting rigid body from the player game object
        playerRb = player.GetComponent<Rigidbody>();

        playerAnimator = player.GetComponent<Animator>();
    }

    private void ProcessSwipe(Vector2 swipeEndPosition)
    {
        if (!isSwipeInProgress)
        {
            Vector2 swipeDirection = swipeEndPosition - swipeStartPosition;

            // Check if swipe magnitude is greater than minimum threshold
            if (swipeDirection.magnitude >= minimumSwipeMagnitude)
            {
                isSwipeInProgress = true;

                // Process swipe direction
                if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                {
                    // Horizontal swipe
                    if (swipeDirection.x < 0)
                    {
                        //Debug.Log("Right swipe detected");
                        RightMove(swipeDirection);
                    }
                    else
                    {
                        //Debug.Log("Left swipe detected");
                        LeftMove(swipeDirection);
                    }
                }
                else
                {
                    // Vertical swipe
                    if (swipeDirection.y < 0)
                    {
                        //Debug.Log("Upward swipe detected");
                        JumpMove(swipeDirection);
                    }
                    else
                    {
                        //Debug.Log("Downward swipe detected");
                        CrouchMove(swipeDirection);
                    }
                }
            }
        }
        else
        {
            isSwipeInProgress = false;
        }
    }

    private void LeftMove(Vector2 swipeDirection)
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

    private void RightMove(Vector2 swipeDirection)
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

    private void JumpMove(Vector2 swipeDirection)
    {
        // Ensure playerRb is not null and there's a Rigidbody component attached
        if (playerRb != null)
        {
            // Calculate the force to apply for left movement
            //Vector3 force = transform.forward * movement_speed * swipeDirection.magnitude;

            // Apply the force to the Rigidbody
            //playerRb.AddForce(force, ForceMode.Impulse);
            Debug.Log("Player has Jumped ..!!!");

            // Activate the Jump mechanism
            playerAnimator.SetTrigger("Jump");
        }
    }

    private void CrouchMove(Vector2 swipeDirection)
    {
        // Ensure playerRb is not null and there's a Rigidbody component attached
        if (playerRb != null)
        {
            // Calculate the force to apply for right movement
            //Vector3 force = -transform.forward * movement_speed * swipeDirection.magnitude;

            // Apply the force to the Rigidbody
            //playerRb.AddForce(force, ForceMode.Impulse);
            Debug.Log("Player has crouched ..!!!");

            // Activate the crouch mechanism
            playerAnimator.SetTrigger("Crouch");
        }
    }

}
