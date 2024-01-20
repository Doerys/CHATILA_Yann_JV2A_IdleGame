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
    public Animator menuEnemyAnimator, spriteEnemyAnimator, heartAnimator, hitAnimator;

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

        menuEnemyAnimator.SetTrigger("Pop");
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
        if (health > 0)
        {
            // on check d'abord si le joueur a s�lectionn� le bon �l�ment avant son attaque. Si oui : on double les d�g�ts

            int vulnerabilityCheck = 1;

            switch (myPlayer.currentElement)
            {
                case PlayerManager.ElementsPlayer.Fire:

                    if (enemyData.elementVulnerability == ElementsPlayer.Fire)
                    {
                        vulnerabilityCheck = 2;
                    }
                    break;

                case PlayerManager.ElementsPlayer.Water:

                    if (enemyData.elementVulnerability == ElementsPlayer.Water)
                    {
                        vulnerabilityCheck = 2;
                    }
                    break;
                case PlayerManager.ElementsPlayer.Thunder:

                    if (enemyData.elementVulnerability == ElementsPlayer.Thunder)
                    {
                        vulnerabilityCheck = 2;
                    }
                    break;
                case PlayerManager.ElementsPlayer.Earth:

                    if (enemyData.elementVulnerability == ElementsPlayer.Earth)
                    {
                        vulnerabilityCheck = 2;
                    }
                    break;
                case PlayerManager.ElementsPlayer.Light:

                    if (enemyData.elementVulnerability == ElementsPlayer.Light)
                    {
                        vulnerabilityCheck = 2;
                    }
                    break;
                default:
                    break;
            }

            health -= powerPlayerAttack * vulnerabilityCheck;

            heartAnimator.SetTrigger("hitTrigger");
            spriteEnemy.sprite = enemyData.spriteHit[levelMonster];

            StartCoroutine(ReturnNormalSprite());

            if (health <= 0)
            {
                health = 0;

                myPlayer.LootGold(enemyData.goldLoot[levelMonster]);
                menuEnemyAnimator.SetTrigger("KillMob");

                StartCoroutine(CooldownSpawnEnemy());
            }

            healthText.text = health.ToString();
        }
    }
    public IEnumerator CooldownSpawnEnemy ()
    {
        yield return new WaitForSeconds(2);
        
        SpawnEnemy();
    }

    public IEnumerator ReturnNormalSprite()
    {
        yield return new WaitForSeconds(.2f);

        spriteEnemy.sprite = enemyData.spriteIdle[levelMonster];
    }

}