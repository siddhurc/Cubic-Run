using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    Rigidbody rb;
    private GameObject player;
    public float activationDistance = 10f;
    private bool playerInVicinity = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (playerInVicinity)
        {
            rb.useGravity = true;
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
