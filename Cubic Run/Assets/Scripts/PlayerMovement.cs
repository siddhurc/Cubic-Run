using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movement_speed = 0.5f;

    // Touch controls
    private TouchInputActions touchInputActions;
    [SerializeField] private float minimumSwipeMagnitude = 6f;
    private Vector2 swipeStartPosition;
    private bool isSwipeInProgress = false;

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
                    if (swipeDirection.x > 0)
                    {
                        Debug.Log("Right swipe detected");
                    }
                    else
                    {
                        Debug.Log("Left swipe detected");
                    }
                }
                else
                {
                    // Vertical swipe
                    if (swipeDirection.y > 0)
                    {
                        Debug.Log("Upward swipe detected");
                    }
                    else
                    {
                        Debug.Log("Downward swipe detected");
                    }
                }
            }
        }
        else
        {
            isSwipeInProgress = false;
        }
    }
}
