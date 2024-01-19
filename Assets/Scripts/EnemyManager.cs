using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class EnemyManager : MonoBehaviour
{
    public EnemyScriptableObject[] allEnemiesDatas;
    private EnemyScriptableObject enemyData;

    public SceneManager dataScene;

    public PlayerManager myPlayer;

    private int levelMonster, health, powerAttack;
    private float timerAttack = 0;

    public bool isActive;

    public Image spriteEnemy, timerEnemyBar;
    public TextMeshProUGUI powerAttackText, healthText, nameText;
    public Animator enemyAnimator, heartAnimator, hitAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myPlayer = FindObjectOfType<PlayerManager>();
        dataScene = FindObjectOfType<SceneManager>();

        if (isActive)
        {
            SpawnEnemy();
        }
        else
        {
            gameObject.SetActive(false);
        }
        
        //StartCoroutine(CooldownSpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if (timerAttack > 0 && health > 0)
        {
            timerAttack -= Time.deltaTime;
            timerEnemyBar.fillAmount = 1 - (timerAttack / enemyData.speedAttack[levelMonster]);

            hitAnimator.SetFloat("cooldownAttack", 1 - (timerAttack / enemyData.speedAttack[levelMonster]));
        }

        else if (health > 0)
        {
            myPlayer.ChangeHealth(powerAttack, -1);
            timerAttack = enemyData.speedAttack[levelMonster];
            hitAnimator.SetTrigger("hitEnemy");
        }
        //LaunchAttack();
    }

    public void SpawnEnemy()
    {
        // Choose randomly one enemy between all existing and make it appear
        enemyData = allEnemiesDatas[Random.Range(0, allEnemiesDatas.Length)];
        levelMonster = enemyData.level[dataScene.currentDifficulty];

        spriteEnemy.GetComponent<Image>().sprite = enemyData.spriteIdle[levelMonster];

        // load statistics
        health = enemyData.health[levelMonster];
        powerAttack = Random.Range(enemyData.minPowerAttack[levelMonster], enemyData.maxPowerAttack[levelMonster]);
        timerAttack = enemyData.speedAttack[levelMonster];

        // load texts
        nameText.text = enemyData.nameMonster[levelMonster];
        healthText.text = health.ToString();
        powerAttackText.text = powerAttack.ToString();
    }

    // timer decreasing before attack
    public void LaunchAttack()
    {


    }

    public void ClickEnemy()
    {
        if (myPlayer.multiHitActive)
        {
            for (int i = 0; i < dataScene.allEnemies.Length; i++)
            {
                if (dataScene.allEnemies[i].isActive)
                {
                    dataScene.allEnemies[i].GetHit(myPlayer.powerClick);
                }
            }
        }
        else
        {
            GetHit(myPlayer.powerClick);
        }
    }

    public void GetHit(int powerPlayerAttack)
    {
        health -= powerPlayerAttack;
        healthText.text = health.ToString();

        heartAnimator.SetTrigger("hitTrigger");
        spriteEnemy.sprite = enemyData.spriteHit[levelMonster];

        StartCoroutine(ReturnNormalSprite());

        if (health <= 0)
        {
            //StartCoroutine(CooldownSpawnEnemy());
            SpawnEnemy();
            myPlayer.LootGold(enemyData.goldLoot[levelMonster]);
        }
    }
    public IEnumerator CooldownSpawnEnemy ()
    {
        yield return new WaitForSeconds(1);

        SpawnEnemy();
    }

    public IEnumerator ReturnNormalSprite()
    {
        yield return new WaitForSeconds(.2f);

        spriteEnemy.sprite = enemyData.spriteIdle[levelMonster];
    }

}