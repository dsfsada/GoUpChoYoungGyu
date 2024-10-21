using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.maxValue = GameObject.Find("Player").GetComponent<Player>().maxHealth;
    }

    void Update()
    {
        //플레이어 체력
        slider.maxValue = GameObject.Find("Player").GetComponent<Player>().maxHealth;
        slider.value = GameObject.Find("Player").GetComponent<Player>().health;
    }
}
