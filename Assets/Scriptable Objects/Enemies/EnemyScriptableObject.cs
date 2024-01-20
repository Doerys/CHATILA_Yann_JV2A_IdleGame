using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "enemy", menuName = "DataEnemy", order = 100)]

public class EnemyScriptableObject : ScriptableObject
{

    public int [] health, minPowerAttack, maxPowerAttack, goldLoot, level;
    public float [] speedAttack;
    public string [] nameMonster;
    public Sprite [] spriteIdle, spriteHit;
    public ElementsEnemy elementEnemy;
    public ElementsPlayer elementVulnerability;

}

public enum ElementsEnemy
{
    Nature,
    Fire,
    Water,
    Earth,
    Shadow,
    Death
}