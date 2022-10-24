using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Revive : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] float reviveEffectsDelay = 2f;
    [SerializeField] GameObject reviveEffect;
    [SerializeField] GameObject mainModel;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();

        _enemy.OnStartSpawn += callback =>
        {
            StopAllCoroutines();
            StartCoroutine(ReviveEffects(reviveEffectsDelay, callback));
        };
    }

    public IEnumerator ReviveEffects(float delay, Action callback)
    {
        //_enemy.enabled = true;
        mainModel.SetActive(false);
        reviveEffect.SetActive(true);
        yield return new WaitForSeconds(delay);
        mainModel.SetActive(true);
        reviveEffect.SetActive(false);
        callback.Invoke();
    }
}
