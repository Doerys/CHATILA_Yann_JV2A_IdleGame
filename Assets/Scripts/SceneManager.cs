using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public EnemyManager[] allEnemies;
    public int currentDifficulty;

    public DiceSystem[] allDices; 

    // Start is called before the first frame update
    void Start()
    {
        currentDifficulty = 0;
    }

    // Update is called once per frame
    public void Update()
    {

    }

}
