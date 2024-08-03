using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public Transform bulletSpawnPoint;

    public int gunDamage = 1;
    public float fireRate = 0.24f;
    public float weaponRange = 30f;
    public float hitForce = 100f;

    private GameObject enemyGO;
    private float nextFire = 0.24f;
    public GameObject bulletGO;
    public float shootForce = 50f;

    Vector3 directionToEnemy;

    public Button fireButton;

    void Start()
    {
        // Register a callback for when the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unregister the callback when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reinitialize references when a new scene is loaded
        enemyGO = GameObject.FindGameObjectWithTag("Enemy");
        fireButton = GameObject.Find("FireButton").GetComponent<Button>();
        fireButton.onClick.AddListener(shootButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void shootButtonPressed()
    {
        //shoot only after a cooldown period of fireRate
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShootBullet());

            //play the animation for the player fire action which is function call in player movement
            gameObject.GetComponentInParent<PlayerMovement>().WeaponFired();
        }
    }

    bool IsPlayerInShootingDistance()
    {
        RaycastHit hit;
        directionToEnemy = (bulletSpawnPoint.position - enemyGO.transform.position).normalized;

        if (Physics.Raycast(bulletSpawnPoint.position, directionToEnemy, out hit, weaponRange))
        {
            Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
    }

    //to be used when the player picks up auto aim and fire enemy power up
    IEnumerator AttackEnemy()
    {
        Debug.Log("Enemy is being attacked");
        GameObject currentBullet = Instantiate(bulletGO, bulletSpawnPoint.transform.position, Quaternion.identity);

        // Rotate bullet to shoot in the direction of the game object
        currentBullet.transform.forward = directionToEnemy.normalized;

        // Add forces to the bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionToEnemy.normalized * shootForce, ForceMode.Impulse);

        yield return null;
    }

    IEnumerator ShootBullet()
    {
        Debug.Log("Bullet is fired!!");
        GameObject currentBullet = Instantiate(bulletGO, bulletSpawnPoint.transform.position, Quaternion.identity);

        // Rotate bullet to shoot straight ahead.
        currentBullet.transform.forward = Vector3.forward.normalized;

        // Add forces to the bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * shootForce, ForceMode.Impulse);

        yield return null;
    }
}
