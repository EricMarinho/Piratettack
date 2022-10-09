using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private ShipData characterData;
    [SerializeField] private Slider healthBar;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public int health { get; private set; }
    private void Start()
    {

        healthBar.maxValue = characterData.maxHealth;
        health = characterData.maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.value = health;
        if (health <= 0)
        {
            spriteRenderer.sprite = characterData.destroyedShipSprite;
        }
        else if (health <= (characterData.maxHealth / 3))
        {
            spriteRenderer.sprite = characterData.veryDeterioratedShipSprite;
        }
        else if (health <= ((characterData.maxHealth * 2) / 3))
        {
            spriteRenderer.sprite = characterData.deterioratedShipSprite;
        }
    }

}
