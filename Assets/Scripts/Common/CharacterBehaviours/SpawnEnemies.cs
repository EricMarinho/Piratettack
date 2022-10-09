using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    private ObjectPooler objectPoolerInstance;
    private PlayerController playerControllerInstance;
    private float spawnTime = 5f;
    private float spawnTimer;
    private int queueSize;
    private int tagsIndex;
    private string[] tags = { "ChaserShip", "ShooterShip" };
    private Transform[] spawnPoints;
    private float distance;
    private float furthestDistance = 0f;
    private Vector3 furthestSpawnPoint;

    private void Start()
    {
        playerControllerInstance = PlayerController.instance;
        spawnTime = PlayerPrefs.GetFloat("SpawnTime", 10f);
        spawnTimer = spawnTime;
        objectPoolerInstance = ObjectPooler.instance;
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnTime)
        {
            spawnTimer = 0;
            tagsIndex = Random.Range(0, tags.Length);
            foreach (Transform spawnPoint in spawnPoints)
            {
                distance = Vector3.Distance(playerControllerInstance.rb.transform.position, spawnPoint.position);
                if (distance > furthestDistance)
                {
                    furthestDistance = distance;
                    furthestSpawnPoint = spawnPoint.position;
                }
            }
            furthestDistance = 0f;
            objectPoolerInstance.poolSpawner.SpawnFromPool(tags[tagsIndex], furthestSpawnPoint, Quaternion.identity);
        }
    }

}
