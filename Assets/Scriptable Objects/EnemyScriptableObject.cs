using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "enemy", menuName = "EnemyData", order = 100)]

public class EnemyScriptableObject : ScriptableObject
{

    public int health, minPowerAttack, maxPowerAttack, goldLoot;
    public float speedAttack;
    public string nameMonster;
    public Sprite sprite;

}
