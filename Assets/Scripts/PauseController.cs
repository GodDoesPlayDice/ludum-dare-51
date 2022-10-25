using CharacterControl;
using StarterAssets;
using UI;
using UI.Screens;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private InputActionsReceiver _inputs;
    private PauseScreen _pauseScreen;
    private UpgradeMenuController _upgradeMenuController;

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        _inputs = GetComponent<InputActionsReceiver>();
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
            _pauseScreen.ToggleWholeScreen(isPause);
        Time.timeScale = IsPaused ? 0f : 1f;
    }
}