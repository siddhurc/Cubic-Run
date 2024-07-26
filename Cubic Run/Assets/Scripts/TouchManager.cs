using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.Gyroscope;

public class TouchManager : MonoBehaviour
{
    // Touch controls
    private TouchInputActions touchInputActions;
    [SerializeField] private float minimumSwipeMagnitude = 2f;
    private Vector2 swipeStartPosition;
    private bool isSwipeInProgress = false;


    //accelerometer initialization
    //Accelerometer accelerometer;
    //[SerializeField] private float tiltSensitivity = 14f;

    public GameObject player;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        touchInputActions = new TouchInputActions();
    }

    private void OnEnable()
    {
        touchInputActions.Enable();

//#if !UNITY_EDITOR
//        //enable accelerometrer
//        InputSystem.EnableDevice(Accelerometer.current);
//#endif
    }

    private void OnDisable()
    {
        touchInputActions.Disable();

//#if !UNITY_EDITOR
//        //disable accelerometer
//        InputSystem.DisableDevice(Accelerometer.current);
//#endif
    }

    private void Start()
    {
        // Subscribe to swipe events
        touchInputActions.TouchInputMovement.Swipe.started += ctx => swipeStartPosition = ctx.ReadValue<Vector2>();
        touchInputActions.TouchInputMovement.Swipe.canceled += ctx => ProcessSwipe(ctx.ReadValue<Vector2>());

        //Getting rigid body from the player game object
        playerMovement = FindObjectOfType<PlayerMovement>();

        //getting the current accelerometer
        //accelerometer = Accelerometer.current;

        //player = GameObject.FindWithTag("Player").transform.Find("body")?.gameObject;
        
    }

    private void Update()
    {
        //Debug.Log("sid_accele_value " + accelerometer.acceleration.ReadValue().x);

        //using preprocessor directives so that this code only runs in unity player i.e. android device
//#if !UNITY_EDITOR
//        float tilt_x = accelerometer.acceleration.ReadValue().x;

//        tilt_x = Mathf.Clamp(tilt_x, -1.0f, 1.0f);

//        Vector3 playerMovement = new Vector3(tilt_x * tiltSensitivity, 0, 0);

//        Debug.Log($"Accelerometer Data: {tilt_x}");

//        player.transform.Translate(playerMovement * Time.deltaTime);
//#endif
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
                        playerMovement?.RightMove(swipeDirection);
                    }
                    else
                    {
                        //Debug.Log("Left swipe detected");
                        playerMovement?.LeftMove(swipeDirection);
                    }
                }
                else
                {
                    // Vertical swipe
                    if (swipeDirection.y < 0)
                    {
                        //Debug.Log("Upward swipe detected");
                        playerMovement?.JumpMove(swipeDirection);
                    }
                    else
                    {
                        //Debug.Log("Downward swipe detected");
                        playerMovement?.CrouchMove(swipeDirection);
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
