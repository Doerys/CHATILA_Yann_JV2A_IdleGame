using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ability", menuName = "DataAbility", order = 100)]

public class AbilityScriptableObject : ScriptableObject
{
    public string nameAbility;
    public int upgradeLevel, costUpgrade;
}
