using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilitySystem : MonoBehaviour
{
    public AbilityScriptableObject dataAbility;
    private int costUpgrade, gold, currentUpgradeStep;
    public TextMeshProUGUI costUpgradeText;

    // Start is called before the first frame update
    void Start()
    {
        costUpgrade = dataAbility.costUpgrade;
        currentUpgradeStep = dataAbility.upgradeStep;
    }

    // Update is called once per frame
    void Update()
    {
        if (gold >= costUpgrade)
        {
            costUpgradeText.color = new Color(0, 0, 0);
        }
        else
        {
            costUpgradeText.color = new Color(255, 0, 0);
        }
    }

    public void UpgradePower()
    {
        if (gold >= costUpgrade)
        {
            costUpgrade += costUpgrade+10;
            currentUpgradeStep += dataAbility.upgradeStep;

            costUpgradeText.text = costUpgrade.ToString();
        }
    }
}
