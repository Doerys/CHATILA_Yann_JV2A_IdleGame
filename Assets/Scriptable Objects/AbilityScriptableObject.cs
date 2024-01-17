using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ability", menuName = "DataAbility", order = 100)]

public class AbilityScriptableObject : ScriptableObject
{
    public string nameAbility;
    public int upgradeLevel, costUpgrade, upgradeStep, costMana;
    public bool useMana;
    public Sprite sprite;

    public void LaunchPower(int upgradeLevel, int costMana, PlayerManager myPlayer)
    {
        if (myPlayer.currentMana >= costMana)
        {
            CalledByAction(upgradeLevel, costMana, myPlayer);
        }
    }

    public virtual void CalledByAction(int upgradeLevel, int costMana, PlayerManager myPlayer)
    {

    }
}
