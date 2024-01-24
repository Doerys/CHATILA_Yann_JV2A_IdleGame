using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiceSystem : AbilitySystem
{
    public DicesScriptableObject dataDice;

    public EnemyManager [] listEnemies;

    public AudioSource diceSound;

    public Image diceRollAlert;
    public TextMeshProUGUI diceRollAlertText;

    public Color elementalColor;

    public Animator animatorDice;

    // Start is called before the first frame update
    void Start()
    {
        // Choose randomly one enemy between all existing and make it appear
        //gameObject.GetComponent<Image>().sprite = diceData.sprite;

        LoadAbility();

        listEnemies = dataScene.allEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateManager();
    }

    public void RollDice()
    {
        if (myPlayer.currentMana >= costMana && !isCharging && !isLocked)
        {
            diceSound.Play();

            //consommation de mana
            myPlayer.ChangeMana(dataAbility.costMana * currentPower, -1);

            int powerDice = 0;

            // on relance les dés autant de fois qu'on a amélioré la compétence
            for (int i = 0;  i < currentPower; i++)
            {
                powerDice += Random.Range(dataDice.minValue, dataDice.maxValue);
            }

            diceRollAlert.sprite = spriteAbility.sprite;
            diceRollAlertText.color = elementalColor;
            //diceRollAlertText.color = Color.blue;

            diceRollAlertText.text = powerDice.ToString();

            animatorDice.SetTrigger("TriggerRoll");

            StartCoroutine(IdleAnimation());

            for (int i = 0; i < listEnemies.Length; i++)
            {

                if (listEnemies[i].isActiveAndEnabled)
                {
                    listEnemies[i].GetHit(powerDice);
                }
            }

            isCharging = true;
        }
    }
    public IEnumerator IdleAnimation()
    {
        yield return new WaitUntil(() => diceRollAlert.color.a == 0);

        animatorDice.SetTrigger("IddleAnimation");
    }
}
