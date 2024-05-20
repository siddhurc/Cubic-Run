using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSectionTrigger : MonoBehaviour
{
    public GameObject obstaclePrefab; // Reference to the obstacle prefab to spawn
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider is the player
        {
            // Instantiate a new prefab obstacle 20 units ahead of the current trigger z position value. 
            Instantiate(obstaclePrefab, new Vector3(0, 0, gameObject.transform.position.z+220f), Quaternion.identity);

            //Disable the new obstacle spawn trigger game object since it spawns multiple obstacles while in contact with player.
            gameObject.SetActive(false);
        }
    }
}
