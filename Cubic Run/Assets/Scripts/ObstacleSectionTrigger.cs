using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSectionTrigger : MonoBehaviour
{
    public ObstacleManager obstacleManager;
    public Transform spawnPoint;

    private void Start()
    {
        obstacleManager = ObstacleManager.FindObjectOfType<ObstacleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider is the player
        {
            Vector3 spawnPoint = new Vector3(0, 0, gameObject.transform.position.z + 220f);
            obstacleManager.spawnNewObstacle(spawnPoint); //inform obstacle manager to spawn new obstacle at the given spawn point

            //Disable the new obstacle spawn trigger game object since it spawns multiple obstacles while in contact with player.
            gameObject.SetActive(false);
        }
    }
}
