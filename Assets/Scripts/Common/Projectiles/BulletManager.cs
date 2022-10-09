using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private BulletData bulletData;
    private Rigidbody2D rb;
    private float timer;
    private ObjectPooler objectPoolerInstance;
    [SerializeField] private TrailRenderer trailRenderer;

    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        objectPoolerInstance = ObjectPooler.instance;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        trailRenderer.Clear();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= bulletData.bulletLifeTime)
        {
            timer = 0;
            objectPoolerInstance.poolSpawner.ReturnToPool(bulletData.bulletTag, gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * bulletData.bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        timer = 0;
        objectPoolerInstance.poolSpawner.ReturnToPool(bulletData.bulletTag, gameObject);
        objectPoolerInstance.poolSpawner.SpawnFromPool("BulletExplosion", rb.transform.position, Quaternion.identity);
    }
}
