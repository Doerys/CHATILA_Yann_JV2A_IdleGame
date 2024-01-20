using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "multiHit", menuName = "DataMultiHitPower", order = 100)]

public class MultiHit : AbilityScriptableObject
{
    protected override void CalledByAction(int power, PlayerManager myPlayer, AbilitySystem powerItself)
    {
        powerItself.LaunchMultiHitCoroutine(power);
    }
}
