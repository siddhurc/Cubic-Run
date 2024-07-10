using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySectionTrigger : MonoBehaviour
{
    private EnvironmentManager environmentManager;
    // Start is called before the first frame update
    void Start()
    {
        environmentManager = EnvironmentManager.FindObjectOfType<EnvironmentManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider is the player
        {
            Vector3 spawnPoint = new Vector3(0, 0, gameObject.transform.position.z + 220f);
            environmentManager.spawnNewSky(spawnPoint); //inform obstacle manager to spawn new obstacle at the given spawn point

            //Disable the new obstacle spawn trigger game object since it spawns multiple obstacles while in contact with player.
            gameObject.SetActive(false);
        }
    }
}
