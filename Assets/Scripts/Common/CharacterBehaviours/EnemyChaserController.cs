using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyChaserController : MonoBehaviour
{

    private PlayerController playerControllerInstance;
    private ObjectPooler objectPoolerInstance;
    private Rigidbody2D rb;
    [SerializeField] private ShipData ChaserData;
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private EnemyDeathManager deathManager;
    private bool isAlive = true;
    private AIPath aiPath;

    private void Start()
    {
        aiPath = GetComponent<AIPath>();
        playerControllerInstance = FindObjectOfType<PlayerController>();
        objectPoolerInstance = ObjectPooler.instance;
    }

    private void Update()
    {
        aiPath.destination = playerControllerInstance.rb.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && isAlive)
        {
            isAlive = false;
            playerControllerInstance.TakeDamage(ChaserData.damage);
            healthManager.TakeDamage(ChaserData.maxHealth);
            deathManager.DestoyShip(2, 0, ChaserData.poolTag);
        }

        if (other.gameObject.CompareTag("PlayerProjectile") && isAlive)
        {
            healthManager.TakeDamage(playerControllerInstance.playerData.bulletDamage);
            if (healthManager.health <= 0)
            {
                isAlive = false;
                deathManager.DestoyShip(2, ChaserData.scoreValue, ChaserData.poolTag);
            }
        }

    }
}
