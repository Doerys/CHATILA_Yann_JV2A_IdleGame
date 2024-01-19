using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ability", menuName = "DataAbility", order = 100)]

public class AbilityScriptableObject : ScriptableObject
{
    public string nameAbility;
    public int upgradeLevel, costUpgrade, power, costMana;
    public float timerCooldown;
    public bool useMana, autoPower;
    public Sprite sprite;

    public void LaunchPower(int power, PlayerManager myPlayer)
    {
        CalledByAction(power, myPlayer);
    }

    public virtual void CalledByAction(int power, PlayerManager myPlayer)
    {

    }
}
