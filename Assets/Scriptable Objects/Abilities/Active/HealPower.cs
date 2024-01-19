using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "heal", menuName = "DataHealPower", order = 100)]

public class HealPower : AbilityScriptableObject
{
    public override void CalledByAction(int power, PlayerManager myPlayer)
    {
        //soin
        myPlayer.ChangeHealth(power, 1);

        //changement d'UI
        myPlayer.ChangeUI(myPlayer.currentHealthText, myPlayer.currentHealth, myPlayer.maxHealth, myPlayer.lifeBar);
    }
}
