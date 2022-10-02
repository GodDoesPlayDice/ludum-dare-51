using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemFiller : MonoBehaviour
{

    public TMP_Text upgradeName;
    public TMP_Text upgradeDescription;
    public RawImage upgradeIcon;

    public void SetName(string name)
    {
        upgradeName.SetText(name);
    }

    public void SetDescription(string description)
    {
        upgradeDescription.SetText(description);
    }

    public void SetIcon(Texture icon)
    {
        upgradeIcon.texture = icon;
        
    }
    
}
