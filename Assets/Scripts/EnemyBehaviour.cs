using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public DicesScriptableObject[] allEnemiesDatas;
    public DicesScriptableObject enemyData;

    // Start is called before the first frame update
    void Start()
    {

        // Choose randomly one enemy between all existing and make it appear
        enemyData = allEnemiesDatas[Random.Range(0, allEnemiesDatas.Length)];
        gameObject.GetComponent<Image>().sprite = enemyData.spriteEnemy;

        // roll a dice score
        enemyScore = Random.Range(enemyData.minValue, enemyData.maxValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
