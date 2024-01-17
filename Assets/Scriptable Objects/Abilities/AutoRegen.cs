using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "autoRegen", menuName = "AutoRegenData", order = 100)]

public class AutoRegen : AbilityScriptableObject
{
    public override void CalledByAction(int upgradeLevel, int costMana, PlayerManager myPlayer)
    {
        
    }
}
