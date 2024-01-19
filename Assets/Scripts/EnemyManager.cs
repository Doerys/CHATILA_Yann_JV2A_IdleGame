using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public EnemyScriptableObject[] allEnemiesDatas;
    private EnemyScriptableObject enemyData;

    public SceneManager dataScene;

    public PlayerManager myPlayer;

    private int health, powerAttack;
    private float timerAttack = 0;

    public bool isActive;

    public Image spriteEnemy, timerEnemyBar;
    public TextMeshProUGUI powerAttackText, healthText, nameText;
    public Animator enemyAnimator;

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
            timerEnemyBar.fillAmount = 1 - (timerAttack / enemyData.speedAttack);
        }

        else if (health > 0)
        {
            myPlayer.ChangeHealth(powerAttack, -1);
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

        enemyAnimator.SetTrigger("HitTrigger");

        if (health <= 0)
        {
            //StartCoroutine(CooldownSpawnEnemy());
            SpawnEnemy();
            myPlayer.LootGold(enemyData.goldLoot);
        }
    }
    public IEnumerator CooldownSpawnEnemy ()
    {
        yield return new WaitForSeconds(1);

        SpawnEnemy();
    }

}