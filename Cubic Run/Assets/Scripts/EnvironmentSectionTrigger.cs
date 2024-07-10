using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSectionTrigger : MonoBehaviour
{
    private EnvironmentManager environmentManager;
    private Transform spawnPoint;

    private void Start()
    {
        environmentManager = EnvironmentManager.FindObjectOfType<EnvironmentManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider is the player
        {
            Vector3 spawnPoint = new Vector3(-17.69f, 0, 24.09f + gameObject.transform.position.z + 220f);
            environmentManager.spawnNewEnvironment(spawnPoint); //inform Environment manager to spawn new Environment at the given spawn point

            //Disable the new Environment spawn trigger game object since it spawns multiple Environments while in contact with player.
            gameObject.SetActive(false);
        }
    }
}
