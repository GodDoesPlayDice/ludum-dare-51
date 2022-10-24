using System;
using System.Collections;
using DG.Tweening;
using UI.Screens;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Death : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)] float deathEffectsDelay = 3f;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] GameObject mainModel;

    private Character _character;

    public event Action<GameObject> OnReadyToPool;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _character.OnIsAliveChange += isAlive =>
        {
            if (isAlive)
            {
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(DeathEffects(deathEffectsDelay));
            }
        };
    }

    public IEnumerator DeathEffects(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        _character.enabled = false;

        if (_character is Enemy)
        {
            var mTransform = mainModel.transform;
            var mStartPos = mTransform.position;
            var mLocalStartPos = mTransform.localPosition;
            mTransform.DOMoveY(mStartPos.y - 2, 5f).SetUpdate(true).OnComplete(() => {
                OnReadyToPool?.Invoke(gameObject);
                transform.position = mTransform.position;
                mTransform.localPosition = mLocalStartPos; // Move model back to 0
            });
            deathParticle?.Play();
        }
        else
        {
            var deathScreen = _character.GetComponentInChildren<DeathScreen>();
            deathScreen.ToggleFullScreen(true);
        }
            
    }
}