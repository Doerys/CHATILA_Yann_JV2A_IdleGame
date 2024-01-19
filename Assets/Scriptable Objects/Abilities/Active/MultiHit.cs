using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "multiHit", menuName = "DataMultiHitPower", order = 100)]

public class MultiHit : AbilityScriptableObject
{
    public override void CalledByAction(int power, PlayerManager myPlayer)
    {
        //soin
        myPlayer.currentHealth += power;

        // si le soin dépasse la limite max de vie, réajuste au maximum
        if (myPlayer.currentHealth > myPlayer.maxHealth)
        {
            myPlayer.currentHealth = myPlayer.maxHealth;
        }

        //changement d'UI
        myPlayer.ChangeUI(myPlayer.currentHealthText, myPlayer.currentHealth, myPlayer.maxHealth, myPlayer.lifeBar);
    }
}
