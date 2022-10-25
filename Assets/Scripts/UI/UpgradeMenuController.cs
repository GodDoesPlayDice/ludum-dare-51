using System.Linq;
using CharacterControl;
using UnityEngine;

public class UpgradeMenuController : MonoBehaviour
{
    public GameObject panelObject;

    private UpgradeManager upgradeManager;
    private UpgradeApplier upgradeApplier;

    private InputActionsReceiver _inputs;
    private PauseController _pauseController;

    private void Awake()
    {
        _inputs = GetComponentInParent<InputActionsReceiver>();
        _pauseController = GetComponentInParent<PauseController>();
    }

    private void Start()
    {
        if (upgradeManager == null)
        {
            InitUpgradeFields();
        }
    }

    private void Update()
    {
        if (!panelObject.activeSelf && _inputs.upgrades)
        {
            _inputs.upgrades = false;
            Show();
        }
    }

    private void InitUpgradeFields()
    {
        var upgradeObj = GameObject.FindGameObjectWithTag("Upgrade");
        upgradeManager = upgradeObj.GetComponent<UpgradeManager>();
        upgradeApplier = upgradeObj.GetComponent<UpgradeApplier>();
    }

    public void FillFields(Upgrade[] upgradeList)
    {
        var upgradeItems = gameObject.GetComponentsInChildren<UpgradeItemFiller>();
        if (upgradeList == null)
        {
            upgradeItems.ToList().ForEach(it => it.Clear());
            return;
        }

        for (int i = 0; i < upgradeList.Length; i++)
        {
            upgradeItems[i].SetUpgrade(upgradeList[i]);
        }
    }

    public void OnUpgradeSelect(Upgrade upgrade)
    {
        FillFields(null);
        upgradeApplier.ApplyUpgrade(upgrade);
        if (!Show())
        {
            Hide();
        }
    }

    public bool Show()
    {
        if (upgradeManager.LevelsAvailable <= 0 || _pauseController.IsPaused)
        {
            return false;
        }

        _pauseController.TogglePause(true);
        panelObject.SetActive(true);

        FillFields(upgradeManager.GetCurrentAvailableUpgrades());
        return true;
    }

    public void Hide()
    {
        panelObject.SetActive(false);
        _pauseController.TogglePause(false);
    }
}