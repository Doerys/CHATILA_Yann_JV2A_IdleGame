using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI costImprovementUI;
    public TextMeshProUGUI costImprovementAutoUI;

    public int scoreGame;
    public int powerClick;
    public int powerAutoClick;
    public int costClick;
    public int costAutoClick;

    private bool autoClick_unlocked = false;

    public GameObject myCanvas;
    public GameObject prefabEnemy;

    public EnemyManager[] allEnemies;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(prefabEnemy, myCanvas.transform);
        }
    }

    public void IncrementScore_Click()
    {
        scoreGame += powerClick;
        goldText.text = "Happiness : " + scoreGame;
    }

    public void IncrementScore_AutoClick(int _powerAutoClick)
    {
        scoreGame += _powerAutoClick;
        goldText.text = "Happiness : " + scoreGame;
    }

    public void SpawnDice()
    {
        //Initiate
        
        //int healthDice
    }

    public void ImproveClick()
    {
        if (scoreGame >= costClick)
        {
            powerClick += 1;
            scoreGame -= costClick;
            costClick *= 5;

            costImprovementUI.text = "Improve Click (-" + costClick + ")";
            goldText.text = "Happiness : " + scoreGame;
        }
    }

    public void ImproveAutoClick()
    {
        if (!autoClick_unlocked)
        {
            autoClick_unlocked = true;
            scoreGame -= 50;
            StartCoroutine(AutoClicker());
            costImprovementAutoUI.text = "Improve Auto Click (-" + costAutoClick + ")";
            goldText.text = "Happiness : " + scoreGame;
        }

        else if (scoreGame >= costAutoClick)
        {
            powerAutoClick += 1;
            scoreGame -= costAutoClick;
            costAutoClick *= 5;

            costImprovementAutoUI.text = "Improve Auto Click (-" + costAutoClick + ")";
            goldText.text = "Happiness : " + scoreGame;
        }
    }

    private IEnumerator AutoClicker()
    {
        while (true)
        {
            IncrementScore_AutoClick(powerAutoClick);
            yield return new WaitForSeconds(1);
        }
    }
}
