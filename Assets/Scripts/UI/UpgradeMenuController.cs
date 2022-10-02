using UnityEngine;

public class UpgradeMenuController : MonoBehaviour
{

    public void FillFields(Upgrade[] upgradeList)
    {
        var upgradeItems = gameObject.GetComponentsInChildren<UpgradeItemFiller>();
        for (int i = 0; i < 2; i++)
        {
            upgradeItems[i].SetName(upgradeList[i].upgradeName);
            upgradeItems[i].SetDescription(upgradeList[i].description);
            upgradeItems[i].SetIcon(upgradeList[i].icon);
        }
    }
    
}
