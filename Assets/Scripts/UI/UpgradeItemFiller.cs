using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemFiller : MonoBehaviour
{

    public TMP_Text upgradeName;
    public TMP_Text upgradeDescription;
    public RawImage upgradeIcon;

    private Upgrade upgrade;
    private UpgradeMenuController menuController;

    private void Start()
    {
        menuController = GetComponentInParent<UpgradeMenuController>();
    }
    public void SetUpgrade(Upgrade upgrade)
    {
        this.upgrade = upgrade;
        SetName(upgrade.upgradeName);
        SetDescription(upgrade.description);
        SetIcon(upgrade.icon);
    }

    public void Clear()
    {
        this.upgrade = null;
        SetName("");
        SetDescription("");
        SetIcon(null);
    }

    public void Selected()
    {
        menuController.OnUpgradeSelect(upgrade);
    }

    private void SetName(string name)
    {
        upgradeName.SetText(name);
    }

    private void SetDescription(string description)
    {
        upgradeDescription.SetText(description);
    }

    private void SetIcon(Texture icon)
    {
        upgradeIcon.texture = icon;
        
    }
    
}
