using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySystem : MonoBehaviour
{
    public AbilityScriptableObject dataAbility;

    [SerializeField]
    public int costUpgrade, currentUpgradeStep;

    public Image spriteAbility;
    public TextMeshProUGUI costUpgradeText, costManaText;

    public PlayerManager myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        costUpgrade = dataAbility.costUpgrade;
        currentUpgradeStep = dataAbility.upgradeStep;

        myPlayer = FindObjectOfType<PlayerManager>();

        spriteAbility.GetComponent<Image>().sprite = dataAbility.sprite;

        costUpgradeText.text = costUpgrade.ToString();
        costManaText.text = dataAbility.costMana.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (myPlayer.currentGold >= costUpgrade)
        {
            costUpgradeText.color = new Color(0, 0, 0);
        }
        else
        {
            costUpgradeText.color = new Color(255, 0, 0);
        }*/
    }

    public void UpgradePower()
    {

        // si assez d'or => retrait d'or, amélioration

        if (myPlayer.currentGold >= costUpgrade)
        {
            myPlayer.currentGold -= costUpgrade;

            costUpgrade += costUpgrade+10;
            currentUpgradeStep += dataAbility.upgradeStep;

            costUpgradeText.text = costUpgrade.ToString();
        }
    }

    public void ClickPower()
    {
        dataAbility.LaunchPower(currentUpgradeStep, dataAbility.costMana, myPlayer);
    }
}
