using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float maxHealth = 15, currentHealth, maxMana = 15, currentMana;
    public int currentGold, powerClick, costUpgradeHealth, costUpgradeMana;

    public int upgradeAmount;

    public bool isAlive, multiHitActive;
    public Image lifeBar, manaBar, gameOverImage;
    public SceneData sceneManager;
    public TextMeshProUGUI currentHealthText, currentManaText, goldText, costUpgradeHealthText, costUpgradeManaText, gameOverText;
    public Animator heartAnimator, manaAnimator, blackScreenAnimator;

    public ElementButton[] allButtonElements;

    public ParticleSystem particleSystemHeal, particleSystemMana;

    public ElementsPlayer currentElement;

    public AudioSource gameOverSound, useManaSound, upgradeSound;

    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<SceneData>();
        currentHealth = maxHealth;
        currentMana = maxMana;
        gameOverText.gameObject.SetActive(false);
        gameOverImage.gameObject.SetActive(false);

        ChangeUI(currentHealthText, currentHealth, maxHealth, lifeBar);
        ChangeUI(currentManaText, currentMana, maxMana, manaBar);

        costUpgradeHealthText.text = costUpgradeHealth.ToString();
        costUpgradeManaText.text = costUpgradeMana.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            // change la couleur du costUpgradeHealthText en rouge si pas assez d'argent
            if (currentGold >= costUpgradeHealth)
            {
                costUpgradeHealthText.color = new Color(255, 255, 255);
            }
            else
            {
                costUpgradeHealthText.color = new Color(255, 0, 0);
            }

            // change la couleur du costUpgradeManaText en rouge si pas assez d'argent
            if (currentGold >= costUpgradeMana)
            {
                costUpgradeManaText.color = new Color(255, 255, 255);
            }
            else
            {
                costUpgradeManaText.color = new Color(255, 0, 0);
            }
        }
    }

    public void ChangeHealth(int power, int damageOrHeal)
    {
        if (isAlive)
        {
            currentHealth += power * damageOrHeal;

            heartAnimator.SetFloat("currentStat", currentHealth / maxHealth);

            if (damageOrHeal > 0)
            {
                particleSystemHeal.Play();
            }

            // si le soin d�passe la limite max de vie, r�ajuste au maximum
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            // si la vie arrive en dessous de 0
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isAlive = false;

                gameOverText.gameObject.SetActive(true);
                gameOverImage.gameObject.SetActive(true);

                blackScreenAnimator.SetTrigger("DeadTrigger");

                StartCoroutine(LaunchGameOverScene());

                gameOverSound.Play();
            }

            ChangeUI(currentHealthText, currentHealth, maxHealth, lifeBar);
        }
    }

    public void ChangeMana(int power, int costManaOrRegen)
    {
        if (isAlive)
        {
            currentMana += power * costManaOrRegen;

            manaAnimator.SetFloat("currentStat", currentMana / maxMana);

            if (costManaOrRegen > 0)
            {
                particleSystemMana.Play();
            }
            else
            {
                useManaSound.Play();
            }

            // si le soin d�passe la limite max de vie, r�ajuste au maximum
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }

            ChangeUI(currentManaText, currentMana, maxMana, manaBar);
        }
    }

    public void UpgradeHealth()
    {
        if (currentGold >= costUpgradeHealth && isAlive)
        {
            upgradeSound.Play();

            currentGold -= costUpgradeHealth;
            goldText.text = currentGold.ToString();

            // le co�t de l'am�lioration augmente proportionnellement
            costUpgradeHealth += costUpgradeHealth;
            costUpgradeHealthText.text = costUpgradeHealth.ToString();

            maxHealth += maxHealth;

            StepUpChallenge();

            ChangeUI(currentHealthText, currentHealth, maxHealth, lifeBar);
        }
    }

    public void UpgradeMana()
    {
        if (currentGold >= costUpgradeMana && isAlive)
        {
            upgradeSound.Play();

            currentGold -= costUpgradeMana;
            goldText.text = currentGold.ToString();

            // le co�t de l'am�lioration augmente proportionnellement
            costUpgradeMana += costUpgradeMana;
            costUpgradeManaText.text = costUpgradeMana.ToString();

            maxMana += maxMana;

            ChangeUI(currentManaText, currentMana, maxMana, manaBar);
        }
    }

    public void StepUpChallenge()
    {
        upgradeAmount++;

        if (upgradeAmount == 1)
        {
            sceneManager.allEnemies[1].gameObject.SetActive(true);
            sceneManager.allEnemies[1].SpawnEnemy();
        }
        else if (upgradeAmount == 5)
        {
            sceneManager.allEnemies[2].gameObject.SetActive(true);
            sceneManager.allEnemies[2].SpawnEnemy();
        }
        else if (upgradeAmount == 15)
        {
            sceneManager.currentDifficulty = 1;
            sceneManager.allEnemies[3].gameObject.SetActive(true);
            sceneManager.allEnemies[3].SpawnEnemy();
        }
        else if (upgradeAmount == 30)
        {
            sceneManager.currentDifficulty = 2;
            sceneManager.allEnemies[4].gameObject.SetActive(true);
            sceneManager.allEnemies[4].SpawnEnemy();
        }
        else if (upgradeAmount == 45)
        {
            sceneManager.currentDifficulty = 3;
            sceneManager.allEnemies[5].gameObject.SetActive(true);
            sceneManager.allEnemies[5].SpawnEnemy();
        }
    }

    public void ChangeUI(TextMeshProUGUI textUI, float currentStat, float maxStat, Image bar)
    {
        textUI.text = currentStat.ToString();
        bar.fillAmount = currentStat / maxStat;
    }

    public void LootGold(int goldLoot)
    {
        currentGold += goldLoot;
        goldText.text = currentGold.ToString();
    }

    public void CheatCode()
    {
        currentGold += 5000000;
        goldText.text = currentGold.ToString();
    }

    public IEnumerator LaunchGameOverScene()
    {
        yield return new WaitUntil(() => sceneManager.blackScreen.color.a == 1);

        SceneManager.LoadScene("GameOverScene");
    }
}
