using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DiceSystem : AbilitySystem
{
    public DicesScriptableObject dataDice;

    public EnemyManager [] listEnemies;

    // Start is called before the first frame update
    void Start()
    {
        // Choose randomly one enemy between all existing and make it appear
        //gameObject.GetComponent<Image>().sprite = diceData.sprite;

        LoadAbility();

        listEnemies = dataScene.allEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateManager();
    }

    public void RollDice()
    {
        if (myPlayer.currentMana >= costMana && !isCharging && !isLocked)
        {
            //consommation de mana
            myPlayer.ChangeMana(dataAbility.costMana * currentPower, -1);

            int powerDice = 0;

            // on relance les dés autant de fois qu'on a amélioré la compétence
            for (int i = 0;  i < currentPower; i++)
            {
                powerDice += Random.Range(dataDice.minValue, dataDice.maxValue);
            }

            for (int i = 0; i < listEnemies.Length; i++)
            {

                if (listEnemies[i].isActiveAndEnabled)
                {
                    listEnemies[i].GetHit(powerDice);
                }
            }

            isCharging = true;
        }
    }
}
