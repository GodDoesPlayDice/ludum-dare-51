using StarterAssets;
using UI;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private StarterAssetsInputs _inputs;
    private MenuController _menuController;

    private bool _isPaused;

    private void Awake()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        _menuController = GetComponentInChildren<MenuController>();
    }

    private void Update()
    {
        if (_inputs.esc)
            TogglePause(!_isPaused);
    }

    public void TogglePause(bool isPause)
    {
        _isPaused = isPause;
        _inputs.esc = false;
        _menuController.ToggleWholeScreen(isPause);
        Time.timeScale = _isPaused ? 0f : 1f;
    }
}