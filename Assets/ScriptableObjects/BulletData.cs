using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletData", order = 2)]
public class BulletData : ScriptableObject
{
    public float bulletSpeed = 15f;
    public float bulletLifeTime = 2f;
    public string colliderTag = "";
    public string bulletTag = "";

}
