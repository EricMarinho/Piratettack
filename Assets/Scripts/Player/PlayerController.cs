using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public ShipData playerData;
    [HideInInspector] public Rigidbody2D rb;

    private HealthManager healthManager;
    private DeathManager deathManager;
    public bool isPlayable = true;

    #region Singleton

    public static PlayerController instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }

    #endregion

    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
        deathManager = GetComponent<DeathManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        if (isPlayable)
        {
            healthManager.TakeDamage(damage);
            if (healthManager.health <= 0)
            {
                deathManager.Die();
            }
        }

    }

}
