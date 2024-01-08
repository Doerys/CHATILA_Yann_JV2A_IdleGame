using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "diceEnemy", menuName = "DicesEnemyData", order = 100)]

public class DicesScriptableObject : ScriptableObject
{

    public int maxValue, minValue;
    public Sprite sprite;

}
