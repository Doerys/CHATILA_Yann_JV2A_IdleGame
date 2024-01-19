using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{
    public float maxHealth = 15, currentHealth, maxMana = 15, currentMana;
    public int currentGold;
    public int powerClick;
    public bool isAlive, multiHitActive;
    public Image lifeBar, manaBar;
    public SceneManager sceneManager;
    public TextMeshProUGUI currentHealthText, currentManaText, goldText;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<SceneManager>();
        currentHealth = maxHealth;
        currentMana = maxMana;

        ChangeUI(currentHealthText, currentHealth, maxHealth, lifeBar);
        ChangeUI(currentManaText, currentMana, maxMana, manaBar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealth(int power, int damageOrHeal)
    {
        currentHealth += power * damageOrHeal;
        ChangeUI(currentHealthText, currentHealth, maxHealth, lifeBar);

        //Debug.Log(healthCurrent + "damage" + damage);

        // si le soin dépasse la limite max de vie, réajuste au maximum
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // si la vie arrive en dessous de 0
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isAlive = false;
        }
    }

    public void ChangeMana(int power, int costManaOrRegen)
    {
        currentMana += power * costManaOrRegen;

        // si le soin dépasse la limite max de vie, réajuste au maximum
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }

        ChangeUI(currentManaText, currentMana, maxMana, manaBar);
    }

    public void ChangeUI(TextMeshProUGUI textUI, float currentStat, float maxStat, Image bar)
    {
        textUI.text = currentStat.ToString();
        bar.fillAmount = currentStat / maxStat;
    }

    public void LootGold(int goldLoot)
    {
        currentGold += goldLoot;
        goldText.text = currentGold.ToString();
    }
}
