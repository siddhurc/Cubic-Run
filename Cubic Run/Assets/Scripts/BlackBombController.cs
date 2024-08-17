using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBombController : MonoBehaviour
{
    Rigidbody rb;
    public float forceMagnitude = 0.5f;
    private GameObject player;
    private GameManager gameManager;
    private bool playerInVicinity = false;
    public float activationDistance = 75f;
    public ParticleSystem explosioPefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameManager.FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        if (playerInVicinity)
        {
            Vector3 force = Vector3.back * forceMagnitude;
            rb.useGravity = true;
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

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManager>().PlayerTakeDamage(7);
            Instantiate(explosioPefab, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
        }
    }
}
