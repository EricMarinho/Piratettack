using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyDeathManager : MonoBehaviour
{

    private ObjectPooler objectPoolerInstance;
    private ScoreHandler scoreHandlerInstance;
    [SerializeField] GameObject burningParticles;

    private void Start()
    {
        objectPoolerInstance = ObjectPooler.instance;
        scoreHandlerInstance = ScoreHandler.instance;
    }

    public void DestoyShip(float time, int scoreToAdd, string poolTag)
    {
        burningParticles.SetActive(true);
        GetComponent<AIPath>().enabled = false;
        scoreHandlerInstance.AddScore(scoreToAdd);
        StartCoroutine(DestroyAfterTime(time, poolTag));
    }

    private IEnumerator DestroyAfterTime(float time, string poolTag)
    {
        yield return new WaitForSeconds(time);
        objectPoolerInstance.poolSpawner.ReturnToPool(poolTag, gameObject);

    }
}