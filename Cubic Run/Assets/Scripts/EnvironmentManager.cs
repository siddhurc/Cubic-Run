using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject environmentPrefab; // Reference to the Environment prefab to spawn
    public GameObject skyPrefab;

    //creating a list to handle all Environments dynamically
    private List<GameObject> environmentList = new List<GameObject>();
    private List<GameObject> skyList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spawnInitialEnvironments();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnNewEnvironment(Vector3 spawnPoint)
    {
        // Instantiate a new prefab Environment 20 units ahead of the current trigger z position value. 
        GameObject newEnvironment = Instantiate(environmentPrefab, spawnPoint, Quaternion.identity);
        addEnvironmentToList(newEnvironment);
        removeOldestEnvironment();
    }

    public void spawnNewSky(Vector3 spawnPoint)
    {
        if (skyList.Count <= 5)
        {
            GameObject newSky = Instantiate(skyPrefab, spawnPoint, Quaternion.identity);
            addSkyToList(newSky);
            removeOldestSky();
        }
    }

    private void addEnvironmentToList(GameObject gameObjectToAdd)
    {
        environmentList.Add(gameObjectToAdd);
    }

    private void addSkyToList(GameObject gameObjectToAdd)
    {
        skyList.Add(gameObjectToAdd);
    }

    private void spawnInitialEnvironments()
    {
        //spawn ossfet due to incorrect multi child object location.
        // correct this in future builds
        Vector3 spawnOffset = new Vector3(-17.69f, 0f, 24.09f);

        //Spawning 4 initial game object Environments
        Vector3 spawnPoint_1 = new Vector3(0f, 0f, 0f);
        Vector3 spawnPoint_2 = new Vector3(0f, 0f, 80f);
        Vector3 spawnPoint_3 = new Vector3(0f, 0f, 160f);
        Vector3 spawnPoint_4 = new Vector3(0f, 0f, 240f);

        spawnNewEnvironment(spawnPoint_1 + spawnOffset);
        spawnNewEnvironment(spawnPoint_2 + spawnOffset);
        spawnNewEnvironment(spawnPoint_3 + spawnOffset);
        spawnNewEnvironment(spawnPoint_4 + spawnOffset);

        spawnNewSky(spawnPoint_1);
        spawnNewSky(spawnPoint_2);
        spawnNewSky(spawnPoint_3);
        spawnNewSky(spawnPoint_4);

    }

    private void removeOldestEnvironment()
    {
        if (environmentList.Count >= 5)
        {
            GameObject oldestEnvironment = environmentList[0];
            environmentList.RemoveAt(0);
            Destroy(oldestEnvironment);
        }
    }

    private void removeOldestSky()
    {
        if (skyList.Count >= 5)
        {
            GameObject oldestSky = skyList[0];
            skyList.RemoveAt(0);
            Destroy(oldestSky);
        }
    }
}
