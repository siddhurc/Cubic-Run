using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMovement : MonoBehaviour
{
    public float movement_speed = 0.5f;

    public TouchActions touchActions;

    public GameObject player;
    private Rigidbody player_rb;

    private void Awake()
    {
        touchActions = new TouchActions();
    }

    private void OnEnable()
    {
        touchActions.Enable();
        //TouchSimulation.Enable();
    }

    private void OnDisable()
    {
        touchActions.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //horizontalMovement = new InputAction(binding: "HorizontalMovement/TouchPosition");
        player_rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = touchActions.HorizontalMovement.TouchPosition.ReadValue<Vector2>().x;

        Debug.Log(moveInput);

        Vector3 moveDirection = new Vector3(moveInput, 0f, 0f);

        player.transform.Translate(moveDirection * Time.deltaTime * movement_speed);
    }

    private void MovePlayer(float movement)
    {
        player_rb.velocity = new Vector3(movement, 0f, 0f);
    }
}

