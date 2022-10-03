using StarterAssets;
using UI;
using UI.Screens;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private StarterAssetsInputs _inputs;
    private PauseScreen _pauseScreen;
    private UpgradeMenuController _upgradeMenuController;

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
        _pauseScreen = GetComponentInChildren<PauseScreen>();
        _upgradeMenuController = GetComponentInChildren<UpgradeMenuController>();
    }

    private void Update()
    {
        if (_inputs.esc && !_upgradeMenuController.panelObject.activeSelf)
            TogglePause(!IsPaused, true);
    }

    public void TogglePause(bool isPause, bool showPauseScreen = false)
    {
        if (isPause && _upgradeMenuController.panelObject.activeSelf)
            return;
        IsPaused = isPause;
        _inputs.esc = false;

        if (showPauseScreen)
            _pauseScreen.ToggleFullScreen(isPause);
        Time.timeScale = IsPaused ? 0f : 1f;
    }
}