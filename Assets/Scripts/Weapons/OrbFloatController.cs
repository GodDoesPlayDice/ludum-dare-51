using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbFloatController : MonoBehaviour
{
    private Action onReach;
    private float nextMoveTime;

    void Update()
    {
        if (nextMoveTime > Time.time)
        {
            
        }
    }

    public void MoveToNextPosition(Vector3 pos, float duration, Action onReach)
    {

    }
}
