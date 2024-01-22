using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneData : MonoBehaviour
{
    public EnemyManager[] allEnemies;
    public int currentDifficulty;

    public DiceSystem[] allDices;

    public Image blackScreen;

    // Start is called before the first frame update
    void Start()
    {
        currentDifficulty = 0;

        StartCoroutine(DisableBlackScreen());
    }

    // Update is called once per frame
    public void Update()
    {

    }

    private IEnumerator DisableBlackScreen()
    {

        yield return new WaitForSeconds(1.5f);

        blackScreen.gameObject.SetActive(false);
    }

}
