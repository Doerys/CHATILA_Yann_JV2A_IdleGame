using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ability", menuName = "DataAbility", order = 100)]

public class AbilityScriptableObject : ScriptableObject
{
    public string nameAbility;
    public int costUpgrade, power, costMana;
    public float timerCooldown;
    public bool unlockOnStart, useMana, autoPower, timedPower, isDice;
    public Sprite sprite;

    public void LaunchPower(int power, PlayerManager myPlayer, AbilitySystem powerItself)
    {
        CalledByAction(power, myPlayer, powerItself);
    }

    protected virtual void CalledByAction(int power, PlayerManager myPlayer, AbilitySystem powerItself)
    {

    }
}
