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

    protected int costUpgrade, currentLevel, currentPower, costMana;
    private float currentCooldown, timerAbility;
    protected bool isCharging, isLocked;

    public Image clickableSpace, spriteAbility, spriteAbilityCooldown, squareAbility, buttonMana, buttonManaBis;
    public TextMeshProUGUI costUpgradeText, costManaText, timerAbilityText, multiplyText;

    public PlayerManager myPlayer;

    public Animator timerTextAnimator;

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

    public void LoadAbility()
    {
        costUpgrade = dataAbility.costUpgrade;
        costUpgradeText.text = costUpgrade.ToString();

        currentCooldown = dataAbility.timerCooldown;

        myPlayer = FindObjectOfType<PlayerManager>();

        dataScene = FindObjectOfType<SceneManager>();

        spriteAbility.GetComponent<Image>().sprite = dataAbility.sprite;
        spriteAbilityCooldown.GetComponent<Image>().sprite = dataAbility.sprite;

        // capacités déjà accessibles au début du jeu
        if (dataAbility.unlockOnStart)
        {
            currentLevel = 1;
            currentPower = dataAbility.power;
            
            if (dataAbility.nameAbility == "AutoMana")
            {
                isCharging = true;
                StartCoroutine(AutoRegen());
            }
        }

        // si utilise du mana : on charge le coût en mana
        if (dataAbility.useMana)
        {
            costMana = dataAbility.costMana;
            costManaText.text = dataAbility.costMana.ToString();
        }

        // si la compétence n'a pas encore été améliorée, on la désactive visuellement
        if (!dataAbility.unlockOnStart)
        {
            if (dataAbility.useMana)
            {
                buttonMana.gameObject.SetActive(false);
                buttonManaBis.gameObject.SetActive(false);
                costManaText.gameObject.SetActive(false);
            }
            if (dataAbility.timedPower)
            {
                timerAbilityText.gameObject.SetActive(false);
            }
            if (dataAbility.isDice)
            {
                multiplyText.gameObject.SetActive(false);
            }

            spriteAbility.gameObject.SetActive(false);
            squareAbility.gameObject.SetActive(false);
        }
    }

    public void UpdateManager()
    {
        if(myPlayer.isAlive)
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
                if (myPlayer.currentMana >= costMana)
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
                    isLocked = false;
                }
                currentCooldown = dataAbility.timerCooldown;
            }
        }
    }

    public void UpgradePower()
    {
        // si assez d'or => retrait d'or, amélioration
        if (myPlayer.currentGold >= costUpgrade && myPlayer.isAlive)
        {
            myPlayer.StepUpChallenge();

            myPlayer.currentGold -= costUpgrade;
            myPlayer.goldText.text = myPlayer.currentGold.ToString();

            // le coût de l'amélioration augmente proportionnellement
            costUpgrade += costUpgrade;
            costUpgradeText.text = costUpgrade.ToString();

            currentLevel += 1;

            // pour les capacités utilisant de la mana, et qui ne sont pas des dés, on augmente proportionnellement leur puissance
            if (dataAbility.useMana && !dataAbility.isDice) 
            {
                if (currentLevel == 1)
                {
                    currentPower += dataAbility.power;

                    if (dataAbility.nameAbility == "Heal")
                    {
                        costMana = dataAbility.costMana * 2;
                    }
                    else if (dataAbility.nameAbility == "MultiHit")
                    {
                        costMana = dataAbility.costMana * currentLevel;
                    }
                }
                else
                {                    
                    if (dataAbility.nameAbility == "Heal")
                    {
                        currentPower += currentPower;
                        costMana += costMana;
                    }
                    else if (dataAbility.nameAbility == "MultiHit")
                    {
                        currentPower++;
                        costMana = costMana * currentLevel;
                    }
                }

                costManaText.text = costMana.ToString();
            }
            // pour toutes les autres capacités, +1
            else
            {
                currentPower++;

                if (dataAbility.isDice)
                {
                    costMana = dataAbility.costMana * currentPower;
                    costManaText.text = costMana.ToString();

                    if (currentLevel == 2)
                    {
                        multiplyText.gameObject.SetActive(true);
                    }
                    if (currentLevel > 1)
                    {
                        multiplyText.text = ("x" + currentPower);
                    }
                }
            }

            // particularité du clickAttack => variable du joueur à changer
            if (dataAbility.nameAbility == "Hit")
            {
                myPlayer.powerClick += dataAbility.power;
            }

            // affichage des UI des capacités débloquées
            if (currentLevel == 1)
            {
                // les autoClickers lancent leurs coroutines infinies
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

                // dégriser / activer les pouvoirs qui viennent d'être activé
                if (dataAbility.useMana)
                {
                    buttonMana.gameObject.SetActive(true);
                    buttonManaBis.gameObject.SetActive(true);
                    costManaText.gameObject.SetActive(true);
                }

                spriteAbility.gameObject.SetActive(true);
                squareAbility.gameObject.SetActive(true);
            }
        }
    }

    public void ClickPower()
    {
        if (myPlayer.currentMana >= costMana && !isCharging && !isLocked)
        {
            //consommation de mana
            myPlayer.ChangeMana(costMana, -1);

            if (!dataAbility.timedPower)
            {
                isCharging = true;
            }

            dataAbility.LaunchPower(currentPower, myPlayer, this);
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

            else if (dataAbility.nameAbility == "AutoMana")
            {
                myPlayer.ChangeMana(currentPower, 1);
            }

            yield return new WaitForSeconds(1);

            if (!myPlayer.isAlive)
            {
                break;
            }
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

            if (!myPlayer.isAlive)
            {
                break;
            }
        }
    }

    public void LaunchMultiHitCoroutine(int power)
    {
        isLocked = true;
        timerAbilityText.gameObject.SetActive(true);
        timerTextAnimator.SetBool("timerActivated", true);

        squareAbility.color = new Color(0, 230, 255);
        spriteAbility.gameObject.SetActive(false);

        myPlayer.multiHitActive = true;

        StartCoroutine(TimerMultiHit(power));
    }

    public IEnumerator TimerMultiHit(int timer)
    {
        for (int i = 0; i < timer +1; i++)
        {
            int timeLeft = timer - i;
            timerAbilityText.text = timeLeft.ToString();

            if (timeLeft == 0)
            {
                myPlayer.multiHitActive = false;
                timerTextAnimator.SetBool("timerActivated", true);
                timerAbilityText.gameObject.SetActive(false);
                spriteAbility.gameObject.SetActive(true);

                squareAbility.color = new Color(255, 255, 255);

                isCharging = true;
            }

            if (!myPlayer.isAlive)
            {
                break;
            }

            yield return new WaitForSeconds(1);
        }
    }
}
