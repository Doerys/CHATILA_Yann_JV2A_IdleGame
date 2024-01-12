using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{

    public float healthMax;
    private float healthCurrent;
    public bool isAlive;
    public Image lifeBar;

    // Start is called before the first frame update
    void Start()
    {
        healthCurrent = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseHealth(int damage)
    {
        healthCurrent -= damage;
        lifeBar.fillAmount = healthCurrent / healthMax;

        //Debug.Log(healthCurrent + "damage" + damage);

        if (healthCurrent <= 0)
        {
            healthCurrent = 0;
            isAlive = false;
        }
    }
}
