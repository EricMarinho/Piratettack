using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosionController : MonoBehaviour
{
    private ObjectPooler objectPoolerInstance;
    private float timer = 0f;
    private float time = 0.3f;

    private void Start()
    {
        objectPoolerInstance = ObjectPooler.instance;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            timer = 0;
            objectPoolerInstance.poolSpawner.ReturnToPool("BulletExplosion", gameObject);
        }
    }
}
