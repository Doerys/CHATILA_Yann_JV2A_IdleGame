using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "dice", menuName = "DataDices", order = 100)]

public class DicesScriptableObject : AbilityScriptableObject
{
    public int maxValue, minValue;
}
