using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI costImprovementUI;
    public TextMeshProUGUI costImprovementAutoUI;

    public int scoreGame;
    public int powerClick;
    public int powerAutoClick;
    public int costClick;
    public int costAutoClick;

    public Vector3 enemyPosition;

    private bool autoClick_unlocked = false;

    public GameObject myCanvas;
    public GameObject prefabEnemy;

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
        scoreUI.text = scoreGame.ToString();
    }

    public void IncrementScore_AutoClick(int _powerAutoClick)
    {
        scoreGame += _powerAutoClick;
        scoreUI.text = scoreGame.ToString();
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
            scoreUI.text = scoreGame.ToString();
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
            scoreUI.text = scoreGame.ToString();
        }

        else if (scoreGame >= costAutoClick)
        {
            powerAutoClick += 1;
            scoreGame -= costAutoClick;
            costAutoClick *= 5;

            costImprovementAutoUI.text = "Improve Auto Click (-" + costAutoClick + ")";
            scoreUI.text = scoreGame.ToString();
        }
    }

    public void createEnemy()
    {
        int selectEnemy = Random.Range(1, 6);

        if (selectEnemy > 0)
        {
            //Instantiate();
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
