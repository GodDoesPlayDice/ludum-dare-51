using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponData data;
    public bool shooting = true;


    private float prevShootTime = 0f;
    //private Vector3 currentTarget; // PRIVATE
    public Transform targetTMP;
    

    void Start()
    {
        
    }

    void Update()
    {
        if (prevShootTime + data.coolDown <= Time.time)
        {
            prevShootTime = Time.time;
            //Shoot(currentTarget);
            Shoot(targetTMP.position);
        }
    }

    public void Shoot(Vector3 target)
    {
        var rotation = Quaternion.LookRotation((target - transform.position).normalized);
        Instantiate(data.prefab, transform.position, rotation, transform);
    }
}
