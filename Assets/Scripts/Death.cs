using System;
using System.Collections;
using DG.Tweening;
using UI.Screens;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)] float deathEffectsDelay = 3f;

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
            transform.DOMoveY(transform.position.y - 2, 10f).SetUpdate(true).OnComplete(() => OnReadyToPool?.Invoke(gameObject));
        else
        {
            var deathScreen = _character.GetComponentInChildren<DeathScreen>();
            deathScreen.ToggleWholeScreen(true);
        }
            
    }
}