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

    private int health, powerAttack, goldLoot;
    private float timerAttack;
    private string nameMonster;

    public Image spriteEnemy, timerEnemyBar;
    public TextMeshProUGUI powerAttackText, healthText, nameText;
    public Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = FindObjectOfType<PlayerManager>();
        StartCoroutine(CooldownSpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if (timerAttack > 0 && health > 0)
        {
            timerAttack -= Time.deltaTime;
            timerEnemyBar.fillAmount = timerAttack / enemyData.speedAttack;
        }

        else if (health > 0)
        {
            myPlayer.LoseHealth(powerAttack);
            timerAttack = enemyData.speedAttack;
        }
        //LaunchAttack();
    }

    public void SpawnEnemy()
    {
        // Choose randomly one enemy between all existing and make it appear
        enemyData = allEnemiesDatas[Random.Range(0, allEnemiesDatas.Length)];
        spriteEnemy.GetComponent<Image>().sprite = enemyData.sprite;

        // load statistics
        health = enemyData.health;
        powerAttack = Random.Range(enemyData.minPowerAttack, enemyData.maxPowerAttack);
        timerAttack = enemyData.speedAttack;

        // load texts
        nameText.text = enemyData.nameMonster;
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

        enemyAnimator.SetTrigger("HitTrigger");

        if (health <= 0)
        {
            StartCoroutine(CooldownSpawnEnemy());
            myPlayer.LootGold(enemyData.goldLoot);
        }
    }
    public IEnumerator CooldownSpawnEnemy ()
    {
        yield return new WaitForSeconds(1);

        SpawnEnemy();
    }

}