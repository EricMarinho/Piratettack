using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "ScriptableObjects/ShipData", order = 1)]
public class ShipData : ScriptableObject
{

    public int maxHealth = 6;
    public int damage = 1;
    public int bulletDamage = 2;
    public int scoreValue = 1;
    public float speed = 2f;
    public float reverse = 2f;
    public float rotationSpeed = 2f;
    public float frontalShotReloadTime = 1f;
    public float sideShotReloadTime = 1f;
    public float attackRange = 5f;
    public string frontalPoolTag = "PlayerFrontalShot";
    public string sidePoolTag = "PlayerSideShot";
    public string poolTag = "Ship";
    public Sprite deterioratedShipSprite;
    public Sprite veryDeterioratedShipSprite;
    public Sprite destroyedShipSprite;

}
