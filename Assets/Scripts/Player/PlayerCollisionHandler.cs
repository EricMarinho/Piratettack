using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private PlayerController playerControllerInstance;
    private void Start()
    {
        playerControllerInstance = PlayerController.instance;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            playerControllerInstance.TakeDamage(1);
        }
    }
}
