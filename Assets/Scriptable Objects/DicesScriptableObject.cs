using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "dice", menuName = "DicesData", order = 100)]

public class DicesScriptableObject : ScriptableObject
{

    public int maxValue, minValue;
    public float cooldown;
    public Sprite sprite;

}
