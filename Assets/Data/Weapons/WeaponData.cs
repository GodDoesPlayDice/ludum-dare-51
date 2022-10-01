using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponData", order = 0)]
public class WeaponData : ScriptableObject
{
    public string name;
    public string description;
    public int cost;
    public float damage;
    public float coolDown;
    public float aoeArea;
    public GameObject prefab;
}
