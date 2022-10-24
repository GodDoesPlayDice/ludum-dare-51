using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.ParticleSystem;

public class Revive : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] float reviveEffectsDelay = 2f;
    [SerializeField][Range(0f, 5f)] float spawnDuration = 0.7f;
    [SerializeField] GameObject reviveEffect;
    [SerializeField] GameObject spawnEffect;
    [SerializeField] GameObject mainModel;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();

        _enemy.OnStartSpawn += callback =>
        {
            StopAllCoroutines();
            StartCoroutine(ReviveEffects(callback));
        };
    }

    public IEnumerator ReviveEffects(Action callback)
    {
        //_enemy.enabled = true;
        mainModel.SetActive(false);
        reviveEffect.SetActive(true);
        yield return new WaitForSeconds(reviveEffectsDelay);
        StartSpawn(callback);
    }

    public void StartSpawn(Action callback)
    {
        mainModel.SetActive(true);
        var ySize = gameObject.GetComponentInParent<Collider>().bounds.size.y;
        //spawnEffect?.SetActive(true);
        //mainModel.transform.localPosition = mainModel.transform.localPosition + new Vector3(0f, -ySize, 0f);
        Debug.Log("Start--- " + (mainModel.transform.localPosition + new Vector3(0f, -ySize, 0f)));
        Debug.Log("Start " + mainModel.transform.localPosition);
        mainModel.transform.DOLocalMoveY(-ySize, spawnDuration).SetEase(Ease.OutQuart).From().OnComplete(() =>
        {
            Debug.Log("End " + mainModel.transform.localPosition);
            reviveEffect.SetActive(false); // TODO: fade out
            //spawnEffect?.SetActive(false);
            callback.Invoke();
        });
    }
}
