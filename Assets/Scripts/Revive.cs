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
    private GameObject _player;
    private ParticleSystem _reviveParticle;
    private ParticleSystem _spawnParticle;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _reviveParticle = reviveEffect.GetComponent<ParticleSystem>();
        _spawnParticle = spawnEffect.GetComponent<ParticleSystem>();

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
        _reviveParticle.Play();
        yield return new WaitForSeconds(reviveEffectsDelay);
        StartSpawn(callback);
    }

    public void StartSpawn(Action callback)
    {
        _reviveParticle.Stop();
        _spawnParticle.Play();
        mainModel.SetActive(true);
        reviveEffect.transform.DOScale(0f, 0.3f);
        var ySize = gameObject.GetComponentInParent<Collider>().bounds.size.y;

        // Do with sequence?
        transform.DOLookAt(_player.transform.position, spawnDuration);
        mainModel.transform.DOLocalMoveY(-(ySize * 1.1f), spawnDuration).SetEase(Ease.OutQuart).From().SetDelay(0.2f).OnComplete(() =>
        {
            Debug.Log("End " + mainModel.transform.localPosition);
            _spawnParticle.Stop();
            callback.Invoke();
        });
    }
}
