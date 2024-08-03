using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    //public GameObject obstaclePrefab; // Reference to the obstacle prefab to spawn
    public GameObject[] listOfObstaclePrefabs;

    //creating a list to handle all obstacles dynamically
    private List<GameObject> obstacleList = new List<GameObject>();

    //explosion prefab gameobject
    public ParticleSystem explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        spawnInitialObstacles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnNewObstacle(Vector3 spawnPoint)
    {
        // Instantiate a new prefab obstacle 20 units ahead of the current trigger z position value.
        GameObject newObstaclePrefab = getRandomObstaclePrefab();
        GameObject newObstacleGO = Instantiate(newObstaclePrefab, spawnPoint, Quaternion.identity);
        addObstacleToList(newObstacleGO);
        removeOldestObstacle();
    }

    private void addObstacleToList(GameObject gameObjectToAdd)
    {
        obstacleList.Add(gameObjectToAdd);
    }

    private void spawnInitialObstacles()
    {
        //Spawning 4 initial game object obstacles
        Vector3 spawnPoint_1 = new Vector3(0f, 0f, 0f);
        Vector3 spawnPoint_2 = new Vector3(0f, 0f, 80f);
        Vector3 spawnPoint_3 = new Vector3(0f, 0f, 160f);
        Vector3 spawnPoint_4 = new Vector3(0f, 0f, 240f);

        spawnNewObstacle(spawnPoint_1);
        spawnNewObstacle(spawnPoint_2);
        spawnNewObstacle(spawnPoint_3);
        spawnNewObstacle(spawnPoint_4);
    }

    private void removeOldestObstacle()
    {
        if (obstacleList.Count >= 5)
        {
            GameObject oldestObstacle = obstacleList[0];
            obstacleList.RemoveAt(0);
            Destroy(oldestObstacle);
        }
    }

    public void simulateExplosion(Vector3 explosionPosition)
    {
        ParticleSystem explosionGameObject = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
        explosionGameObject.Emit(50);
        //destrying the game object after 1.9 seconds which is time to play the explosion
        Destroy(explosionGameObject.gameObject, 1.9f);
    }

    public GameObject getRandomObstaclePrefab()
    {
        int obstacleCount = listOfObstaclePrefabs.Length;
        int randomIndex = Random.Range(0, obstacleCount);

        Debug.Log(randomIndex);
        return listOfObstaclePrefabs[randomIndex];
    }
}
