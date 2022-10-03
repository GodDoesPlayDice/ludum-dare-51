using UnityEngine;

public class UpgradeMenuController : MonoBehaviour
{
    public GameObject panelObject;

    private UpgradeManager upgradeManager;
    private UpgradeApplier upgradeApplier;

    private void Start()
    {
        if (upgradeManager == null) {
            InitUpgradeFields();
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
        for (int i = 0; i < upgradeList.Length; i++)
        {
            upgradeItems[i].SetName(upgradeList[i].upgradeName);
            upgradeItems[i].SetDescription(upgradeList[i].description);
            upgradeItems[i].SetIcon(upgradeList[i].icon);
        }
    }
    
    public void Show()
    {
        if (upgradeManager.LevelsAvailable <= 0)
        {
            return;
        }
        panelObject.SetActive(true);

        FillFields(upgradeManager.GetCurrentAvailableUpgrades());
    }

    public void Hide()
    {
        panelObject.SetActive(false);
    }
}
