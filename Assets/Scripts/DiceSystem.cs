using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSystem : PowerSystem
{
    public DicesScriptableObject diceData;

    private int maxValue, minValue, powerDice;

    // Start is called before the first frame update
    void Start()
    {
        // Choose randomly one enemy between all existing and make it appear
        //gameObject.GetComponent<Image>().sprite = diceData.sprite;

        // load statistics
        maxValue = diceData.maxValue;
        minValue = diceData.minValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollDice()
    {
        powerDice = Random.Range(diceData.minValue, diceData.maxValue);
    }
}
