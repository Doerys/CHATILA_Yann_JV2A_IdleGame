using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public EnemyScriptableObject[] allEnemiesDatas;
    private EnemyScriptableObject enemyData;

    public PlayerManager myPlayer;

    public Image spriteEnemy, timerEnemyBar;
    public TextMeshProUGUI powerAttackText, healthText, nameText;

    private int health, powerAttack, goldLoot;
    private float speedAttack;
    private string nameMonster;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = FindObjectOfType<PlayerManager>();
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (speedAttack > 0)
        {
            speedAttack -= Time.deltaTime;
            timerEnemyBar.fillAmount = speedAttack / enemyData.speedAttack;
        }

        else
        {
            myPlayer.LoseHealth(powerAttack);
            speedAttack = enemyData.speedAttack;
        }
        //LaunchAttack();
    }

    public void SpawnEnemy()
    {
        // Choose randomly one enemy between all existing and make it appear
        enemyData = allEnemiesDatas[Random.Range(0, allEnemiesDatas.Length)];
        spriteEnemy.GetComponent<Image>().sprite = enemyData.sprite;

        // load statistics
        nameMonster = enemyData.nameMonster;
        health = enemyData.health;
        powerAttack = Random.Range(enemyData.minPowerAttack, enemyData.maxPowerAttack);
        speedAttack = enemyData.speedAttack;
        goldLoot = enemyData.goldLoot;

        nameText.text = nameMonster;
        healthText.text = health.ToString();
        powerAttackText.text = powerAttack.ToString();
    }

    // timer decreasing before attack
    public void LaunchAttack()
    {


    }

    public void GetHit()
    {
        health--;
        healthText.text = health.ToString();

        if (health <= 0)
        {
            SpawnEnemy();
            myPlayer.LootGold(goldLoot);
        }
    }
}
