using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyShooterController : MonoBehaviour
{

    private PlayerController playerControllerInstance;
    private Rigidbody2D rb;
    [SerializeField] private ShipData ShooterData;
    [SerializeField] private HealthManager healthManager;
    private Vector3 playerDirection;
    private float playerDistance;
    private float angle;
    private float reloadTimer = 0f;
    private bool isReady = true;
    private bool isAlive = true;
    private AIPath aiPath;
    private ObjectPooler objectPoolerInstance;
    [SerializeField] private EnemyDeathManager deathManager;

    void Start()
    {
        objectPoolerInstance = ObjectPooler.instance;
        aiPath = GetComponent<AIPath>();
        playerControllerInstance = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isReady)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= ShooterData.frontalShotReloadTime)
            {
                reloadTimer = 0f;
                isReady = true;
            }
        }
        if (isReady)
        {
            reloadTimer = 0f;
            if (aiPath.reachedEndOfPath && isAlive)
            {
                isReady = false;
                objectPoolerInstance.poolSpawner.SpawnFromPool(ShooterData.frontalPoolTag, rb.transform.position, rb.transform.rotation);
            }
        }
        aiPath.destination = playerControllerInstance.rb.transform.position;
    }

    void FixedUpdate()
    {
        playerDirection = playerControllerInstance.rb.transform.position - rb.transform.position;
        angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && isAlive)
        {
            playerControllerInstance.TakeDamage(ShooterData.damage);
            healthManager.TakeDamage(playerControllerInstance.playerData.damage);
            if (healthManager.health <= 0)
            {
                isAlive = false;
                GetComponent<EnemyShooterController>().enabled = false;
                deathManager.DestoyShip(2, ShooterData.scoreValue, ShooterData.poolTag);
            }
        }
        if (other.gameObject.CompareTag("PlayerProjectile") && isAlive)
        {
            healthManager.TakeDamage(playerControllerInstance.playerData.bulletDamage);
            if (healthManager.health <= 0)
            {
                isAlive = false;
                GetComponent<EnemyShooterController>().enabled = false;
                deathManager.DestoyShip(2, ShooterData.scoreValue, ShooterData.poolTag);
            }
        }
    }

}
