using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{

    public float healthMax = 15, healthCurrent;
    public int currentGold, currentMana;
    public bool isAlive;
    public Image lifeBar, manaBar;
    public SceneManager sceneManager;
    public TextMeshProUGUI goldText;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<SceneManager>();
        healthCurrent = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseHealth(int damage)
    {
        healthCurrent -= damage;
        ChangeHealthUI();

        //Debug.Log(healthCurrent + "damage" + damage);

        if (healthCurrent <= 0)
        {
            healthCurrent = 0;
            isAlive = false;
        }
    }

    public void ChangeHealthUI()
    {
        lifeBar.fillAmount = healthCurrent / healthMax;
    }

    public void LootGold(int goldLoot)
    {
        currentGold += goldLoot;
        goldText.text = currentGold.ToString();
    }

    public void ConsumeMana(int costMana)
    {
        currentMana -= costMana;
    }
}
