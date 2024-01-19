using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiceSystem : AbilitySystem
{
    public DicesScriptableObject dataDice;

    public EnemyManager [] listEnemies;

    private int maxValue, minValue, powerDice;

    // Start is called before the first frame update
    void Start()
    {
        // Choose randomly one enemy between all existing and make it appear
        //gameObject.GetComponent<Image>().sprite = diceData.sprite;

        LoadAbility();

        // load statistics
        maxValue = dataDice.maxValue;
        minValue = dataDice.minValue;

        listEnemies = dataScene.allEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateManager();
    }

    public void RollDice()
    {
        powerDice = Random.Range(dataDice.minValue, dataDice.maxValue);

        for (int i = 0; i < listEnemies.Length; i++)
        {
            listEnemies[i].GetHit(powerDice);
        }

        isCharging = true;
    }
}
