using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    public WeaponData[] allWeapons;
    public WeaponData defaultWeapon;
    public GameObject weaponPrefab;
    public Character targetToFollow;
    public Vector3 positionOffset;

    private HashSet<WeaponData> currentWeapons = new HashSet<WeaponData>();

    void Start()
    {
        AddWeapon(defaultWeapon);
    }

    void Update()
    {
        // TODO: Smooth movement
        transform.position = targetToFollow.gameObject.transform.position + positionOffset;
    }

    public void AddWeapon(WeaponData weapon)
    {
        if (!currentWeapons.Add(weapon))
        {
            var weaponControllers = gameObject.GetComponentsInChildren<WeaponController>();
            var weaponObject = weaponControllers.Where(it => it.data == weapon).First().gameObject;
            GameObject.Destroy(weaponObject);
        }
        var instance = CreateWeaponInstance();
        instance.GetComponent<WeaponController>().data = weapon;
    }

    private GameObject CreateWeaponInstance()
    {
        var result = GameObject.Instantiate(weaponPrefab, transform.position, transform.rotation, transform);

        return result;
    }
}
