using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    Rigidbody rb;
    public float forceMagnitude = 15f;
    private GameObject player;
    private bool playerInVicinity = false;
    public float activationDistance = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if(playerInVicinity)
        {
            Vector3 force = Vector3.back * forceMagnitude;
            rb.AddForce(force);
        }
        else
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer < activationDistance)
            {
                playerInVicinity = true;
            }
        }
    }
}
