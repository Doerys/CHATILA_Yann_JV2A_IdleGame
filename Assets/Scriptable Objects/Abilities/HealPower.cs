using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "heal", menuName = "DataHealPower", order = 100)]

public class HealPower : AbilityScriptableObject
{
    public override void CalledByAction(int upgradeLevel, int costMana, PlayerManager myPlayer)
    {
        if (myPlayer.healthCurrent < myPlayer.healthMax)
        {
            //soin
            myPlayer.healthCurrent += upgradeLevel;

            //consommation de mana
            myPlayer.ConsumeMana(costMana);

            // si le soin dépasse la limite max de vie, réajuste au maximum
            if (myPlayer.healthCurrent > myPlayer.healthMax)
            {
                myPlayer.healthCurrent = myPlayer.healthMax;
            }

            //changement d'UI
            myPlayer.ChangeHealthUI();
        }
    }
}
