using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSystem : AbilitySystem
{
    private int maxValue, minValue, powerDice;

    // Start is called before the first frame update
    void Start()
    {
        // Choose randomly one enemy between all existing and make it appear
        //gameObject.GetComponent<Image>().sprite = diceData.sprite;

        // load statistics
        //maxValue = dataAbility.maxValue;
        //minValue = dataAbility.minValue;

        UpgradePower();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollDice()
    {
        /*powerDice = Random.Range(dataAbility.minValue, dataAbility.maxValue);*/
    }
}
