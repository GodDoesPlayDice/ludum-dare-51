using DG.Tweening;
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
    public float minMovementTime = 1f;
    public float maxMovementTime = 3f;

    private HashSet<WeaponData> currentWeapons = new HashSet<WeaponData>();

    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
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
        var wController = instance.GetComponent<WeaponController>();
        wController.SetData(weapon);
        StartOrbMovement(wController);
    }

    private GameObject CreateWeaponInstance()
    {
        var result = GameObject.Instantiate(weaponPrefab, transform.position, transform.rotation, transform);

        return result;
    }

    private void StartOrbMovement(WeaponController orb)
    {
        var bnd = boxCollider.size / 2;
        var dest = new Vector3(Random.Range(-bnd.x, bnd.x),
            Random.Range(-bnd.y, bnd.y), Random.Range(-bnd.z, bnd.z));
        orb.gameObject.transform.DOLocalMove(dest, Random.Range(minMovementTime, maxMovementTime))
            .SetEase(Ease.InOutSine)
            .OnComplete(() => StartOrbMovement(orb));
    }
}
