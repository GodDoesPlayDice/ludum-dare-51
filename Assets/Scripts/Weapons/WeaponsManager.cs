using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponsManager : MonoBehaviour
{
    public WeaponData[] allWeapons;
    public WeaponData defaultWeapon;
    public GameObject weaponPrefab;
    public Vector3 positionOffset;
    public float minMovementTime = 1f;
    public float maxMovementTime = 3f;

    [SerializeField] private float _damageMultiplier = 1f;
    [SerializeField] private float _critChancePercent = 0f;
    [SerializeField] private float _critMultiplier = 0f;


    private HashSet<WeaponData> currentWeapons = new HashSet<WeaponData>();

    private BoxCollider boxCollider;
    private Character _targetToFollow;

    private void Awake()
    {
        _targetToFollow = GameObject.FindWithTag("Player").GetComponent<Character>(); 
    }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        AddWeapon(defaultWeapon);
    }

    void Update()
    {
        // TODO: Smooth movement
        transform.position = _targetToFollow.gameObject.transform.position + positionOffset;
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
        wController.SetDamageModifierFunction(CalcDamage);
        StartOrbMovement(wController);
    }

    public void IncreaseDamagePercent(float addDamagePercent)
    {
        _damageMultiplier += addDamagePercent / 100f;
    }

    public void IncreaseCritChance(float addCrit)
    {
        _critChancePercent += addCrit;
    }

    public void IncreaseCritMultiplierPercent(float addCritMultiPercent)
    {
        _critMultiplier += addCritMultiPercent / 100f;
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

    private float CalcDamage(float weaponDamage)
    {
        var crit = Random.Range(0, 100) <= _critChancePercent;
        var nonCrit = weaponDamage * _damageMultiplier;
        return crit ? nonCrit * _critMultiplier : nonCrit;
    }
}
