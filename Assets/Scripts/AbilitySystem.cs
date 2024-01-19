using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using Unity.VisualScripting;

public class AbilitySystem : MonoBehaviour
{
    public AbilityScriptableObject dataAbility;

    public SceneManager dataScene;

    private int costUpgrade, currentLevel, currentPower;
    private float currentCooldown;
    protected bool isCharging, isLocked;

    public Image clickableSpace, spriteAbility, spriteAbilityCooldown, squareAbility;
    public TextMeshProUGUI costUpgradeText, costManaText;

    public PlayerManager myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        LoadAbility();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateManager();

    }

    public void UpdateManager()
    {
        // affichage UI upgrade
        if (myPlayer.currentGold >= costUpgrade)
        {
            costUpgradeText.color = new Color(255, 255, 255);
        }
        else
        {
            costUpgradeText.color = new Color(255, 0, 0);
        }

        // affichage UI mana
        if (dataAbility.useMana)
        {
            if (myPlayer.currentMana >= dataAbility.costMana)
            {
                costManaText.color = new Color(255, 255, 255);
            }
            else
            {
                costManaText.color = new Color(255, 0, 0);
            }
        }
        // cooldown visuel de la compétence

        if (isCharging && currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            squareAbility.fillAmount = 1 - (currentCooldown / dataAbility.timerCooldown);
            spriteAbility.fillAmount = 1 - (currentCooldown / dataAbility.timerCooldown);
        }
        else if (isCharging && currentCooldown <= 0)
        {
            if (!dataAbility.autoPower)
            {
                isCharging = false;
            }    
            currentCooldown = dataAbility.timerCooldown;
        }
    }

    public void LoadAbility()
    {
        costUpgrade = dataAbility.costUpgrade;
        currentPower = dataAbility.power;

        currentCooldown = dataAbility.timerCooldown;

        myPlayer = FindObjectOfType<PlayerManager>();

        dataScene = FindObjectOfType<SceneManager>();

        spriteAbility.GetComponent<Image>().sprite = dataAbility.sprite;
        spriteAbilityCooldown.GetComponent<Image>().sprite = dataAbility.sprite;

        costUpgradeText.text = costUpgrade.ToString();

        if (dataAbility.useMana)
        {
            costManaText.text = dataAbility.costMana.ToString();
        }

        if (dataAbility.name != "Hit")
        {
            currentLevel = 0;
            // griser / désactiver les pouvoirs qui n'ont pas été améliorés
        }
    }

    public void UpgradePower()
    {
        // si assez d'or => retrait d'or, amélioration
        if (myPlayer.currentGold >= costUpgrade)
        {
            myPlayer.currentGold -= costUpgrade;

            costUpgrade += costUpgrade+10;
            currentPower += dataAbility.power;
            currentLevel += 1;

            costUpgradeText.text = costUpgrade.ToString();

            if (currentLevel == 1)
            {
                if (dataAbility.autoPower)
                {
                    isCharging = true;
                    if (dataAbility.nameAbility == "AutoHeal")
                    {
                        StartCoroutine(AutoRegen());
                    }
                    else if (dataAbility.nameAbility == "AutoMana")
                    {
                        StartCoroutine(AutoRegen());
                    }
                    else if (dataAbility.nameAbility == "AutoAttack")
                    {
                        StartCoroutine(AutoAttack());
                    }
                }

                // dégriser / activer les pouvoirs qui n'ont pas été améliorés
            }
        }
    }

    public void ClickPower()
    {
        if (myPlayer.currentMana >= dataAbility.costMana && !isCharging)
        {
            //consommation de mana
            myPlayer.ChangeMana(dataAbility.costMana, -1);

            //isLocked = true;
            isCharging = true;

            dataAbility.LaunchPower(currentPower, myPlayer);
        }
        else if (!isCharging)
        {
            // else animation sur le coût en mana
        }
    }

    private IEnumerator AutoRegen()
    {
        while (true)
        {
            if (dataAbility.nameAbility == "AutoHeal")
            {
                myPlayer.ChangeHealth(currentPower, 1);
            }

            else if (dataAbility.nameAbility == "ManaHeal")
            {
                myPlayer.ChangeMana(currentPower, 1);
            }

            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator AutoAttack()
    {
        while (true)
        {
            for (int i = 0; i < dataScene.allEnemies.Length; i++)
            {
                dataScene.allEnemies[i].GetHit(currentPower);
            }
            
            yield return new WaitForSeconds(1);
        }
    }
}
