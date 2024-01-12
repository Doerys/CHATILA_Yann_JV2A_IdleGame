using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public EnemyScriptableObject[] allEnemiesDatas;
    private EnemyScriptableObject enemyData;

    public PlayerManager myPlayer;

    private int health, powerAttack, goldLoot;
    private float speedAttack;
    private string nameMonster;

    // Start is called before the first frame update
    void Start()
    {
        // Choose randomly one enemy between all existing and make it appear
        enemyData = allEnemiesDatas[Random.Range(0, allEnemiesDatas.Length)];
        gameObject.GetComponent<Image>().sprite = enemyData.sprite;

        // load statistics
        health = enemyData.health;
        powerAttack = Random.Range(enemyData.minPowerAttack, enemyData.maxPowerAttack);
        speedAttack = enemyData.speedAttack;
        goldLoot = enemyData.goldLoot;
    }

    // Update is called once per frame
    void Update()
    {
        LaunchAttack();
    }

    // timer decreasing before attack
    public void LaunchAttack()
    {

        if (speedAttack > 0)
        {
            speedAttack -= Time.deltaTime;
        }

        else
        {
            //myPlayer;
            //speedAttack = enemyData.speedAttack;
        }
        

        
    }
}
