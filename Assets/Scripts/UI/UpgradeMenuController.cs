using System.Linq;
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
        if (upgradeManager.LevelsAvailable <= 0)
        {
            return false;
        }
        panelObject.SetActive(true);

        FillFields(upgradeManager.GetCurrentAvailableUpgrades());
        return true;
    }

    public void Hide()
    {
        panelObject.SetActive(false);
    }
}
