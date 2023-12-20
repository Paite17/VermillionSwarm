using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Create Upgrade")]
public class Upgrades : ScriptableObject
{
    [SerializeField] private string upgradeName;
    [SerializeField] private string upgradeDescription;
    [SerializeField] private Image upgradeIcon;

    public string UpgradeName
    {
        get { return upgradeName; }
    }

    public string UpgradeDescription
    {
        get { return upgradeDescription; }
    }

    public Image UpgradeIcon
    {
        get { return upgradeIcon; }
    }
}
