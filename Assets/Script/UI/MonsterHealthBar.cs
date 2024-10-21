using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthBar : MonoBehaviour
{
    public Slider slider;
    private EnemyState enemy;

    private void Start()
    {
        enemy = GetComponent<EnemyState>();
        slider.maxValue = enemy.health;
    }

    void Update()
    {
        slider.value = enemy.health;
    }
}
