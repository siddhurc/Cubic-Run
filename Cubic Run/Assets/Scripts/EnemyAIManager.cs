using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAIManager : MonoBehaviour
{

    public float detectionRadius = 10f;
    public float attackDistance = 1.5f;
    public float moveSpeed = 3f;
    public float attachCooldown = 2f;

    private GameObject player;
    private bool isAttacking = false;
    private int health = 100;

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log(distanceToPlayer);

        if(distanceToPlayer <= detectionRadius)
        {
            MoveTowardsPlayer(distanceToPlayer);
        }
    }

    void MoveTowardsPlayer(float distanceToPlayer)
    {
        if(distanceToPlayer > attackDistance)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else if(!isAttacking)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        Debug.Log("Player is being attacked");

        yield return new WaitForSeconds(attachCooldown);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Bullet"))
        {
            takeDamage(25);
        }
    }

    public void takeDamage(int damageValue)
    {
        health -= damageValue;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            updateHealthUI();
        }
    }

    private void updateHealthUI()
    {
        healthBar.value = health;
    }

}
